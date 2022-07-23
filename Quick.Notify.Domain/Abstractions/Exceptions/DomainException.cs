using Quick.Notification.Domain.Abstractions.Models.Errors;
using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Quick.Notification.Domain.Abstractions.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class DomainException : Exception
    {
        public const string TitleDefaultMessageError = "unexpected error";
        public const string DetailsDefaultMessageError = "an unexpected error occurred";

        protected DomainException()
        {
        }
        protected DomainException(string message) : base(message)
        {
        }
        protected DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public virtual string Title => TitleDefaultMessageError;
        public virtual string Detail => DetailsDefaultMessageError;
        public virtual HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
        public virtual Error Error => new Error
        {
            Errors = ImmutableList.Create(new InnerError
            {
                Title = Title,
                Detail = Detail,
                Status = ((int)StatusCode).ToString(),
            })
        };
    }
}

