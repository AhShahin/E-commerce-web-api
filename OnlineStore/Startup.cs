using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineStore.Helpers;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using QuestPDF.Infrastructure;

namespace OnlineStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication(Configuration);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            QuestPDF.Settings.License = LicenseType.Community;
            
            app.UseSerilogRequestLogging();

            /*app.UseCors(builder => builder
                //.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                //.WithOrigins("")
                //.WithExposedHeaders("Pagination")
                //.WithHeaders("Pagination", "accept", "content-type", "origin")
                );*/


            /*using var scope = app.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<DataContext>();
            await Extensions.Seed(context);*/

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            
        }
    }
}