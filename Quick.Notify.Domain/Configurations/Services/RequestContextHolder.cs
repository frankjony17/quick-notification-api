using Quick.Notify.Domain.Abstractions.Services;
using System;

namespace Quick.Notify.Domain.Configurations.Services
{
    public class RequestContextHolder : IRequestContextHolder
    {
        public Guid CorrelationId { get; set; }
        public object RequestBody { get; set; }
    }
}

