using FluentValidation.Results;
using System;
using System.Net;

namespace Quick.Notify.Domain.Configurations.Models
{
    public class InnerError
    {
        public InnerError()
        {
            // needed for deserialization
        }

        private InnerError(string title, string detail, HttpStatusCode statusCode)
        {
            Title = title;
            Detail = detail;
            Status = ((int)statusCode).ToString();
        }

        public string Title { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }

        public static InnerError FromDefault(Exception ex, HttpStatusCode statusCode) =>
            new InnerError("unexpected error", ex.Message, statusCode);

        public static InnerError FromValidation(ValidationFailure vex) =>
            new InnerError($"validation error on field '{vex.PropertyName}'", vex.ErrorMessage, HttpStatusCode.UnprocessableEntity);
    }
}

