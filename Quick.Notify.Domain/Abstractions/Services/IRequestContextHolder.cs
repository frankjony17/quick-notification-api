using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Notification.Domain.Abstractions.Services
{
    public interface IRequestContextHolder
    {
        public Guid CorrelationId { get; set; }

        public object RequestBody { get; set; }
    }
}
