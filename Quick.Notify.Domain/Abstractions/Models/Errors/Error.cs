using Quick.Notify.Domain.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;

namespace Quick.Notify.Domain.Abstractions.Models.Errors
{
    public class Error
    {
        public Error()
        {
            // needed for deserialization
        }
        private Error(InnerError innerError)
        {
            Errors = ImmutableList.Create(innerError);
        }
        public IEnumerable<InnerError> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode
        {
            get
            {
                var status = Errors.FirstOrDefault()?.Status;
                if (string.IsNullOrWhiteSpace(status))
                {
                    return (int)HttpStatusCode.InternalServerError;
                }
                return int.TryParse(status, out var result)
                    ? result
                    : (int)HttpStatusCode.InternalServerError;
            }
        }
        public static Error FromDefault(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
            new Error(InnerError.FromDefault(ex, statusCode));
        public static Error FromDomain(DomainException ex) =>
           new Error(InnerError.FromDomain(ex));
    }

}

