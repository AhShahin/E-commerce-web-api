using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data.UOW;
using OnlineStore.Data;
using OnlineStore.Helpers.ErrorHandeling;
using System.Configuration;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OnlineStore.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("VipOnly", policy => policy.RequireRole("VIP"));
                options.AddPolicy("AspManager", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
                });
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                        options.SerializerSettings.
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup));
            /* services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            }); */
            // for client credential flow
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // identity server url
                    options.Authority = "https://localhost:7273";
                    options.Audience = "OnlineStore";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
            // for calling another api     
            services.AddHttpClient();    
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("RedisConnection").GetValue<string>("Configuration");
                options.InstanceName = Configuration.GetSection("RedisConnection").GetValue<string>("InstanceName");
            });
            /*builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:4300");
                                  });
            });*/

            return services;
        }
    }
}
