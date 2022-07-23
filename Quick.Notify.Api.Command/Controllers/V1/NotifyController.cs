using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Quick.Notify.Domain.Abstractions.Services;
using Quick.Notify.Domain.Constants;
using Quick.Notify.Domain.Notify.Commands.Requests;
using Quick.Notify.Domain.Notify.Model;
using System;
using System.Threading.Tasks;


namespace Quick.Notify.Api.Command.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/notifies/payment")]
    [Produces(HttpHeader.JsonContentType)]
    [Consumes(HttpHeader.JsonContentType)]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IMediator _mediator;
        private readonly IRequestContextHolder _requestContextHolder;

        public NotifyController(ILogger<NotifyController> logger,
                                           IMediator mediator, 
                                           IRequestContextHolder requestContextHolder)
        {
            _logger = logger;
            _mediator = mediator;
            _requestContextHolder = requestContextHolder;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NotifyRequest request)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Notify received. Protocolo = [{request.Protocolo}]");
            var command = new NotifyCommandRequest(request);
            if (command.IsValid)
            {
                var result = await _mediator.Send(command);
                _logger.LogInformation("Notify processed.");
                return Ok(result);
            }
            else
                return BadRequest(command.ValidationResult);
        }
    }
}
