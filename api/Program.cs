using api.Database;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using api.Repository;
using api.Repository.interfaces;
using api.Services;
using api.Services.interfaces;
using Asp.Versioning;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddAuthorizationBuilder();
//builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
//builder.Services.AddAWSService<IAmazonS3>();


builder.Services.AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(4);
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;

    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy=CookieSecurePolicy.None;
});


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

builder.Services.AddScoped<ICacheService, CacheService>();

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkSpaceService, WorkSpaceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Workhub API",
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workhub API");
    });
}

app.MapIdentityApi<User>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseHttpsRedirection();


app.Run();

