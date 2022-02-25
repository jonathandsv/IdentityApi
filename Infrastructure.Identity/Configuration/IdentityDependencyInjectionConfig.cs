using Infrastructure.Identity.Interfaces.Services;
using Infrastructure.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.Configuration
{
    public static class IdentityDependencyInjectionConfig
    {
        public static IServiceCollection IdentityResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILogoutService, LogoutService>();

            return services;
        }
    }
}