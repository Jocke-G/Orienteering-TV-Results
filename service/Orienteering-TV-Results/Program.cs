using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace OrienteeringTvResults
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Logger.Initialize();

            var host = CreateWebHostBuilder(args).Build();

            try
            {
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Logger.LogFatal("Failed to start application", exception);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
