using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quick.Notification.Infra.CrossCutting.IoC.Modules;

namespace Quick.Notification.Infra.CrossCutting.IoC
{
    public static class IoC
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            DomainModule.Register(services, configuration);
            InfrastructureModule.Register(services, configuration);
            return services;
        }
    }
}

