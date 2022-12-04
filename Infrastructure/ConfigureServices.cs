using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Domain.Identity.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)//, IConfiguration configuration
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("DbContextConnString"));
                });
            services.AddIdentityCore<AppUser>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                   
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateLifetime = true,
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
            });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
