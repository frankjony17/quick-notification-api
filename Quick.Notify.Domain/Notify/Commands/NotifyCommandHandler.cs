using MediatR;
using Microsoft.Extensions.Logging;
using Quick.Notify.Domain.Configurations.Commands;
using Quick.Notify.Domain.Notify.Commands.Requests;
using Quick.Notify.Domain.Notify.Commands.Responses;
using Quick.Notify.Domain.Notify.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Notify.Domain.Notify.Commands
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
