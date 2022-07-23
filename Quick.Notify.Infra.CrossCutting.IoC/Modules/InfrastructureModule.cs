using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeonSource.Infra.Extensions;
using NeonSource.Infra.Logging.Extensions;
using NeonSource.Infra.MessagingBroker;

namespace Quick.Notification.Infra.CrossCutting.IoC.Modules
{
    public static class InfrastructureModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddNeonSourceAclServices()
                .AddNeonSourceMessagingBroker();

        }

        public static IHostBuilder UseCustomLogging(this IHostBuilder hostBuilder, bool removeOtherLoggingProviders = true) =>
            hostBuilder.UseNeonSourceLogging(removeOtherLoggingProviders);
    }
}

