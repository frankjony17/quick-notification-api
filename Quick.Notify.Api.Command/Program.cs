using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Quick.Notification.Infra.CrossCutting.IoC.Modules;

namespace Quick.Notification.Api.Command
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseCustomLogging();
    }
}

