using Quick.Notification.Domain.Abstractions.Services;
using System;

namespace Quick.Notification.Domain.Configurations.Services
{
    public class RequestContextHolder : IRequestContextHolder
    {
        public Guid CorrelationId { get; set; }
        public object RequestBody { get; set; }
    }
}

