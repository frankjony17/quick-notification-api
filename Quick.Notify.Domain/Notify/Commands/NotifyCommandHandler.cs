using MediatR;
using Microsoft.Extensions.Logging;
using Quick.Notification.Domain.Configurations.Commands;
using Quick.Notification.Domain.Notify.Commands.Requests;
using Quick.Notification.Domain.Notify.Commands.Responses;
using Quick.Notification.Domain.Notify.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Notification.Domain.Notify.Commands
{

    public class NotifyCommandHandler :
    IRequestHandler<NotifyCommandRequest, NotifyCommandResponse>

    {
        private readonly ILogger<NotifyCommandHandler> _logger;
        private readonly INotifyService _notifyService;


        public NotifyCommandHandler(ILogger<NotifyCommandHandler> logger,
            INotifyService notifyService
          )
        {
            _logger = logger;
            _notifyService = notifyService;

        }
        public Task<NotifyCommandResponse> Handle(NotifyCommandRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            _logger.LogInformation("Process Begin...");
            var result = _notifyService.Process(request);
            _logger.LogInformation("Process completed...");
            return Task.FromResult(result);
        }

    }
}
