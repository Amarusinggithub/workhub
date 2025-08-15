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





var builder = WebApplication.CreateBuilder(args);



builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});


builder.Services.AddOpenApi();

//add authentication via cookie or header
 builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "DualScheme";
                options.DefaultChallengeScheme = "DualScheme";
            })
            .AddPolicyScheme("DualScheme", "JWT or Cookie", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    // If we have Authorization header - use JWT, fallback to Cookies
                    string authorizationHeader = context.Request.Headers["Authorization"]!;
                    return !string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ")
                        ? JwtBearerDefaults.AuthenticationScheme
                        : CookieAuthenticationDefaults.AuthenticationScheme;
                };
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/api/auth/login";
                options.LogoutPath = "/api/auth/logout";

                options.Cookie.Name = "AuthenticationCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["AppSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"]!)),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // this Read token from "jwtToken" cookie
                        var token = context.Request.Cookies["ACCESS_TOKEN"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });


//turns routes to lower case so controller path doest affect it
 builder.Services.Configure<RouteOptions>(options =>
 {
     options.LowercaseUrls = true;
     options.LowercaseQueryStrings = true;
 });


builder.Services.AddAuthorizationBuilder();
builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentity<User,Role>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(4);
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 1;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;

    })
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
    .AddApiEndpoints();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddControllers();

// cookie policies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy=CookieSecurePolicy.None;
});

// configure Cacheing
builder.Services.AddStackExchangeRedisCache(options =>
{
    string? connect = builder.Configuration.GetConnectionString("Redis");
    options.Configuration =connect;
    if (options.Configuration != null)
        options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
        {
            AbortOnConnectFail = true,
            EndPoints = { options.Configuration }
        };
});

// configure Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddMvc().AddApiExplorer(options =>
{
    options.SubstituteApiVersionInUrl = true;
    options.DefaultApiVersion = new ApiVersion(1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.GroupNameFormat="'v'VVV";
});



// Configure rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);    // Time window of 1 minute
        opt.PermitLimit = 100;                   // Allow 100 requests per minute
        opt.QueueLimit = 2;                      // Queue limit of 2
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});




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
builder.Services.AddScoped<ITokenService, TokenService>();
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


// configures cor policy
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:myAllowSpecificOrigins,
        b =>
        {
            b.WithOrigins("http://localhost",
                    "http://localhost:5173",
                    "https://localhost:5174",
                    "http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Workhub-API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workhub-API");
    });
}

app.MapIdentityApi<User>();
//app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseCookiePolicy();
app.UseRouting();
app.MapControllers();
 app.UseRateLimiter();
// app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

// app.UseSession();
//app.UseResponseCompression();
 app.UseResponseCaching();
app.Run();

