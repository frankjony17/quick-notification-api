using Quick.Notify.Domain.Configurations.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;

namespace Quick.Notify.Domain.Configurations.Models
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

        private Error(IEnumerable<InnerError> innerErrors)
        {
            Errors = innerErrors;
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

        public static Error FromDefault(Exception ex) =>
            new Error(InnerError.FromDefault(ex, HttpStatusCode.InternalServerError));

        public static Error FromValidation(ValidationException vex) =>
            new Error(vex.GetErrors());
    }
}

