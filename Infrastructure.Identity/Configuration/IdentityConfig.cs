﻿using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("IdentityApi")));
                //options.UseSqlServer(@"Server=127.0.0.1,1440;Database=Master;User Id=SA;Password=Adm-1089;TrustServerCertificate=true",
                //x => x.MigrationsAssembly("IdentityApi")));

            services.AddIdentity<ApplicationIdentityUser, IdentityRole<int>>(opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
