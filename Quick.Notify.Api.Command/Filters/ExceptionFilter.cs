using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NeonSource.Infra.Abstractions.Exceptions;
using NeonSource.Infra.Abstractions.Logging;
using Quick.Notification.Domain.Abstractions.Exceptions;
using Quick.Notification.Domain.Abstractions.Models.Errors;
using Quick.Notification.Domain.Abstractions.Services;
using System;
using System.Linq;

namespace Quick.Notification.Api.Command.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IRequestContextHolder _requestContextHolder;
        public ExceptionFilter(ILogger<ExceptionFilter> logger, IRequestContextHolder requestContextHolder)
        {
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var res = ResolveResponse(ex);
            LogException(ex, res);
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(res);
            context.HttpContext.Response.StatusCode = res.StatusCode;
        }
        private static Error ResolveResponse(Exception ex) => ex switch
        {
            DomainException dex => Error.FromDomain(dex),
            HttpException hex => Error.FromDefault(ex, hex.HttpStatus.Value),
            _ => Error.FromDefault(ex)
        };
        private void LogException(Exception ex, Error error)
        {
            var message = error?.Errors?.Any() == true ?
               string.Join("; ", error?.Errors?.Select(t => t.Detail)) :
               ex.Message;
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogError(ex, message);
        }
    }

}

