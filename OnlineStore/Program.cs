using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace OnlineStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Starting Web Host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            /*.UseKestrel(options =>
            {
                options.Listen(IPAddress.Loopback, 5000);  // http:localhost:5000
                options.Listen(IPAddress.Any, 80);         // http:*:80
                options.Listen(IPAddress.Loopback, 443, listenOptions =>
                {
                    listenOptions.UseHttps("onlinestore.pfx", "password");
                });
            })*/
            .ConfigureKestrel(options => {
                var port = 443;
                var pfxFilePath = @"/https/onlinestore.pfx";
                var pfxPassword = "password";

                options.Listen(IPAddress.Any, port, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    listenOptions.UseHttps(pfxFilePath, pfxPassword);
                });
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
                        loggerConfiguration.ReadFrom
                        .Configuration(hostingContext.Configuration))
            .UseStartup<Startup>();
    }
}