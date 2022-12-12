using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreGoodsWebAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                ConfigureSerilog(host);
                host.Run();
                Log.Information("Application Started");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application faild to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void ConfigureSerilog(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var config = services.GetRequiredService<IConfiguration>();
                    var env = services.GetRequiredService<IWebHostEnvironment>();
                    var logfolder = config["LogPath"];
                    if (!Directory.Exists(logfolder))
                    {
                        logfolder = Path.Combine(env.WebRootPath, logfolder);
                        Directory.CreateDirectory(logfolder);
                    }
                    var path = Path.Combine(logfolder, "log.txt");
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .File(path: path,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                    ).CreateLogger();
                }
                catch (Exception ex)
                {
                    File.AppendAllText("apperror.txt", ex.Message);
                }
            }

        }
    }
}
