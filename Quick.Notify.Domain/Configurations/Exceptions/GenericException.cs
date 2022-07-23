using Quick.Notify.Domain.Configurations.Models;
using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using static System.Net.HttpStatusCode;

namespace Quick.Notify.Domain.Configurations.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public sealed class GenericException : DomainException
    {
        public GenericException(string message, HttpStatusCode statusCode = InternalServerError)
            : base(message)
        {
            HttpStatusCodeException = statusCode;
        }

        public GenericException(string message, Exception innerEx, HttpStatusCode statusCode = InternalServerError)
            : base(message, innerEx)
        {
            HttpStatusCodeException = statusCode;
        }

        private GenericException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpStatusCode HttpStatusCodeException { get; set; }

        public override Error Error => new Error
        {
            Errors = ImmutableList.Create(new InnerError
            {
                Title = "an error occurred",
                Detail = Message,
                Status = ((int)HttpStatusCodeException).ToString()
            })
        };
    }
}

