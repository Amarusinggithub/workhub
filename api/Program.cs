using System.Text;
using System.Threading.RateLimiting;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Data.interfaces;
using api.Helpers;
using api.Services;
using api.Services.Auth;
using api.Services.Auth.interfaces;
using api.Services.Boards;
using api.Services.Boards.interfaces;
using api.Services.Infanstructure;
using api.Services.interfaces;
using api.Services.Issues.interfaces;
using api.Services.Projects;
using api.Services.Tasks;
using api.Services.Users;
using api.Services.Users.interfaces;
using api.Services.Workspaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Starting web application");

    ConfigureServices(builder);

    var app = builder.Build();

    ConfigurePipeline(app);

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    // Health checks
    builder.Services.AddHealthChecks()
        .AddDbContext<ApplicationDbContext>()
        .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });

    // Security headers
    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Workhub-API",
                Version = "v1",
                Description = "Workhub API for project management"
            });

            // JWT Authorization in Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    ConfigureAuthentication(builder);

    builder.Services.Configure<RouteOptions>(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

    builder.Services.AddAuthorizationBuilder();
    builder.Services.AddHttpContextAccessor();

    // Exception handling
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));

    // Identity
    ConfigureIdentity(builder);

    // Database
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));

        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
        }
    });

    builder.Services.AddControllers();

    // Cookie policies
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.None
            : CookieSecurePolicy.Always;
        options.Cookie.Name = "__Host-AuthCookie";
    });

    ConfigureCaching(builder);

    ConfigureApiVersioning(builder);

    ConfigureRateLimiting(builder);

    RegisterRepositories(builder);
    RegisterServices(builder);

    ConfigureCors(builder);
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "DualScheme";
        options.DefaultChallengeScheme = "DualScheme";
    })
    .AddPolicyScheme("DualScheme", "JWT or Cookie", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            string? authorizationHeader = context.Request.Headers.Authorization;
            return !string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ")
                ? JwtBearerDefaults.AuthenticationScheme
                : CookieAuthenticationDefaults.AuthenticationScheme;
        };
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.AccessDeniedPath = "/api/auth/access-denied";

        options.Cookie.Name = "__Host-AuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.None
            : CookieSecurePolicy.Always;

        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer not configured"),
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"]
                ?? throw new InvalidOperationException("JWT Audience not configured"),
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"]
                ?? throw new InvalidOperationException("JWT Secret not configured"))),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(5),
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.NameIdentifier
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["ACCESS_TOKEN"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\":\"Unauthorized\"}");
            }
        };
    });
}

void ConfigureIdentity(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 12;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 4;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = !builder.Environment.IsDevelopment();
        options.SignIn.RequireConfirmedAccount = !builder.Environment.IsDevelopment();
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();
}

void ConfigureCaching(WebApplicationBuilder builder)
{
    var redisConnection = builder.Configuration.GetConnectionString("Redis");
    if (!string.IsNullOrEmpty(redisConnection))
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnection;
            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
            {
                AbortOnConnectFail = false,
                ConnectRetry = 3,
                ConnectTimeout = 5000,
                EndPoints = { redisConnection }
            };
        });
    }
    else
    {
        builder.Services.AddMemoryCache();
        Log.Warning("Redis connection string not found, using in-memory cache");
    }
}

void ConfigureApiVersioning(WebApplicationBuilder builder)
{
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new HeaderApiVersionReader("X-Version"),
            new QueryStringApiVersionReader("version"),
            new UrlSegmentApiVersionReader()
        );
    }).AddMvc().AddApiExplorer(options =>
    {
        options.SubstituteApiVersionInUrl = true;
        options.DefaultApiVersion = new ApiVersion(1);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.GroupNameFormat = "'v'VVV";
    });
}

void ConfigureRateLimiting(WebApplicationBuilder builder)
{
    builder.Services.AddRateLimiter(options =>
    {
        // Global rate limit
        options.AddFixedWindowLimiter("Global", opt =>
        {
            opt.Window = TimeSpan.FromMinutes(1);
            opt.PermitLimit = 1000;
            opt.QueueLimit = 10;
            opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        });

        // API rate limit
        options.AddFixedWindowLimiter("Api", opt =>
        {
            opt.Window = TimeSpan.FromMinutes(1);
            opt.PermitLimit = 100;
            opt.QueueLimit = 5;
            opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        });

        options.AddFixedWindowLimiter("Auth", opt =>
        {
            opt.Window = TimeSpan.FromMinutes(1);
            opt.PermitLimit = 10;
            opt.QueueLimit = 0;
        });

        options.OnRejected = async (context, token) =>
        {
            context.HttpContext.Response.StatusCode = 429;
            await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Try again later.", token);
        };
    });
}

void ConfigureCors(WebApplicationBuilder builder)
{
    const string allowSpecificOrigins = "_allowSpecificOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowSpecificOrigins,
            policy =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    policy.WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:5173",
                            "https://localhost:5174"
                             "https://localhost:80")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }
                else
                {
                    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins")
                        .Get<string[]>() ?? Array.Empty<string>();

                    policy.WithOrigins(allowedOrigins)
                        .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
                        .WithHeaders("Content-Type", "Authorization", "X-Version")
                        .AllowCredentials();
                }
            });
    });
}

void RegisterRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
    builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
    builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
    builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
    builder.Services.AddScoped<IBillingRepository, BillingRepository>();
    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IUserInvitationRepository, UserInvitationRepository>();
    builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
    builder.Services.AddScoped<IWorkSpaceSettingsRepository, WorkSpaceSettingsRepository>();
    builder.Services.AddScoped<IWorkSpaceAccessControlRepository, WorkSpaceAccessControlRepository>();
    builder.Services.AddScoped<IWorkSpaceMemberRepository, WorkSpaceMemberRepository>();
    builder.Services.AddScoped<INotificationPreferenceRepository, NotificationPreferenceRepository>();
    builder.Services.AddScoped<IUsageTrackingRepository, UsageTrackingRepository>();
    builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
    builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
    builder.Services.AddScoped<IProjectSettingsRepository, ProjectSettingsRepository>();
    builder.Services.AddScoped<IBoardRepository, BoardRepository>();
    builder.Services.AddScoped<IBoardColumnRepository, BoardColumnRepository>();
    builder.Services.AddScoped<IBoardFilterRepository, BoardFilterRepository>();
    builder.Services.AddScoped<IBoardSortRepository, BoardSortRepository>();
    builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
    builder.Services.AddScoped<IReportRepository, ReportRepository>();
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
    builder.Services.AddScoped<ITaskAssignmentRepository, TaskAssignmentRepository>();
    builder.Services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
    builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
    builder.Services.AddScoped<ITaskLabelRepository, TaskLabelRepository>();
    builder.Services.AddScoped<ITaskAttachmentRepository, TaskAttachmentRepository>();
    builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();
    builder.Services.AddScoped<IWebhookRepository, WebhookRepository>();
    builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();
}

void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
    builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
    builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
    builder.Services.AddScoped<ICacheService, CacheService>();
    builder.Services.AddScoped<IBillingService, BillingService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IInvoiceService, InvoiceService>();
    builder.Services.AddScoped<IUserInvitationService, UserInvitationService>();
    builder.Services.AddScribed<ITokenService, TokenService>();
    builder.Services.AddScoped<IAuditLogService, AuditLogService>();
    builder.Services.AddScoped<IWorkSpaceSettingsService, WorkSpaceSettingsService>();
    builder.Services.AddScoped<IWorkSpaceAccessControlService, WorkSpaceAccessControlService>();
    builder.Services.AddScoped<IWorkSpaceMemberService, WorkSpaceMemberService>();
    builder.Services.AddScoped<INotificationPreferenceService, NotificationPreferenceService>();
    builder.Services.AddScoped<IUsageTrackingService, UsageTrackingService>();
    builder.Services.AddScoped<IStorageService, StorageService>();
    builder.Services.AddScoped<IUserActivityService, UserActivityService>();
    builder.Services.AddScoped<IUserRoleService, UserRoleService>();
    builder.Services.AddScoped<IProjectMemberService, ProjectMemberService>();
    builder.Services.AddScoped<IProjectSettingsService, ProjectSettingsService>();
    builder.Services.AddScoped<IRateLimiterService, RateLimiterService>();
    builder.Services.AddScoped<IBoardService, BoardService>();
    builder.Services.AddScoped<IBoardColumnService, BoardColumnService>();
    builder.Services.AddScoped<IBoardFilterService, BoardFilterService>();
    builder.Services.AddScoped<IBoardSortService, BoardSortService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
    builder.Services.AddScoped<IReportService, ReportService>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
    builder.Services.AddScoped<ITaskStatusService, TaskStatusService>();
    builder.Services.AddScoped<ITaskCommentService, TaskCommentService>();
    builder.Services.AddScoped<ITaskLabelService, TaskLabelService>();
    builder.Services.AddScoped<ITaskAttachmentService, TaskAttachmentService>();
    builder.Services.AddScoped<ISubtaskService, SubtaskService>();
    builder.Services.AddScoped<IWebhookService, WebhookService>();
    builder.Services.AddScoped<IIntegrationService, IntegrationService>();
    builder.Services.AddScoped<IAIService, AIService>();
}

void ConfigurePipeline(WebApplication app)
{
    app.UseExceptionHandler();

    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

        if (!app.Environment.IsDevelopment())
        {
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        }

        await next();
    });

    // Health checks
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var response = new
                {
                    Status = report.Status.ToString(),
                    Checks = report.Entries.Select(entry => new
                    {
                        Name = entry.Key,
                        Status = entry.Value.Status.ToString(),
                        Description = entry.Value.Description,
                        Duration = entry.Value.Duration.TotalMilliseconds
                    }),
                    TotalDuration = report.TotalDuration.TotalMilliseconds
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkHub API v1");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
                options.EnableTryItOutByDefault();
                options.DefaultModelsExpandDepth(-1);
            });
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }
    }

    app.UseForwardedHeaders();

    app.UseCors("_allowSpecificOrigins");

    app.UseCookiePolicy();

    app.UseRouting();

    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseResponseCaching();

    app.MapIdentityApi<User>();
    app.MapControllers();

   app.MapGet("/", () => new
       {
           Message = "WorkHub API",
           Version = "v1.0",
           Status = "Running",
           Environment = app.Environment.EnvironmentName,
           Timestamp = DateTime.UtcNow,
           Links = new
           {
               Documentation = "/swagger",
               Health = "/health",
               Ready = "/health/ready"
           }
       })
       .WithName("Root")
       .WithOpenApi();
}
