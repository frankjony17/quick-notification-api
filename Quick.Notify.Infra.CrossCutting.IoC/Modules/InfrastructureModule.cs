using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CompanySource.Infra.Extensions;
using CompanySource.Infra.Logging.Extensions;
using CompanySource.Infra.MessagingBroker;

namespace Quick.Notify.Infra.CrossCutting.IoC.Modules
{
    public static class InfrastructureModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCompanySourceAclServices()
                .AddCompanySourceMessagingBroker();

        }

        public static IHostBuilder UseCustomLogging(this IHostBuilder hostBuilder, bool removeOtherLoggingProviders = true) =>
            hostBuilder.UseCompanySourceLogging(removeOtherLoggingProviders);
    }
}

