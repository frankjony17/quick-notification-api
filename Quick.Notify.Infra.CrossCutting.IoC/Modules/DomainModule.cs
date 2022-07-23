using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quick.Notification.Domain.Abstractions.Services;
using Quick.Notification.Domain.Configurations.Services;
using Quick.Notification.Domain.Notify.Commands;
using Quick.Notification.Domain.Notify.Commands.Requests;
using Quick.Notification.Domain.Notify.Commands.Responses;
using Quick.Notification.Domain.Notify.Services;

namespace Quick.Notification.Infra.CrossCutting.IoC.Modules
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

