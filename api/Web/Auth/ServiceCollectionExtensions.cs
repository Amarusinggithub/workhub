using api.Models;
using api.Services.Auth;
using Microsoft.AspNetCore.Identity;

namespace api.Web.Auth;

public static class ServiceCollectionExtensions
{
    public static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSender<User>, CustomNoOpEmailSender>();
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<TokenService>();
    }
}
