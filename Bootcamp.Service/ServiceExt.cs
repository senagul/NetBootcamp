using Bootcamp.Repository;
using Bootcamp.Repository.Identities;
using Bootcamp.Service.ExceptionHandlers;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Token;
using Bootcamp.Service.Users;
using Bootcamp.Service.Weather;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bootcamp.Service
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddProductService();

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.Configure<CustomTokenOptions>(configuration.GetSection("CustomTokenOptions"));
            services.Configure<Clients>(configuration.GetSection("Clients"));

            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<UserService>();
            services.AddIdentityExt();
            services.AddAuthenticationExt(configuration);
        }

        public static async Task SeedIdentityData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            await UserSeedData.Seed(userManager, roleManager);
        }

        public static void AddIdentityExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<AppDbContext>();
        }

        public static void AddAuthenticationExt(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var tokenOptions = configuration.GetSection("CustomTokenOptions").Get<CustomTokenOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateIssuer = true,

                    ValidAudiences = tokenOptions.Audience,
                    ValidateAudience = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Signature)),
                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true
                };
            });
        }
    }
}
