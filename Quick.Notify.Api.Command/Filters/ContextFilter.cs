using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Quick.Notify.Domain.Abstractions.Services;
using Quick.Notify.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quick.Notify.Api.Command.Filters
{    public class ContextFilter : IActionFilter
    {
        private readonly ILogger<ContextFilter> _logWriter;
        private readonly IRequestContextHolder _requestContextHolder;

        public ContextFilter(ILogger<ContextFilter> logWriter, IRequestContextHolder requestContextHolder)
        {
            _logWriter = logWriter;
            _requestContextHolder = requestContextHolder;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var correlationId = headers[HttpHeader.CorrelationIdHeader];
            var parsed = Guid.TryParse(correlationId, out var guid) ? guid : Guid.NewGuid();

            _logWriter.BeginCorrelationIdScope(parsed);
            _requestContextHolder.CorrelationId = parsed;
        }
    }
}
