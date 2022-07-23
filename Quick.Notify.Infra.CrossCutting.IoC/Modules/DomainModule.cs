using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quick.Notify.Domain.Abstractions.Services;
using Quick.Notify.Domain.Configurations.Services;
using Quick.Notify.Domain.Notify.Commands;
using Quick.Notify.Domain.Notify.Commands.Requests;
using Quick.Notify.Domain.Notify.Commands.Responses;
using Quick.Notify.Domain.Notify.Services;

namespace Quick.Notify.Infra.CrossCutting.IoC.Modules
{
    public static class DomainModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestContextHolder, RequestContextHolder>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<IRequestHandler<NotifyCommandRequest, NotifyCommandResponse>, NotifyCommandHandler>();
        }
    }
}

