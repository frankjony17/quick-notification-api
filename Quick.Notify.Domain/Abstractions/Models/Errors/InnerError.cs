using Quick.Notify.Domain.Abstractions.Exceptions;
using System;
using System.Net;

namespace Quick.Notify.Domain.Abstractions.Models.Errors
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
        public static InnerError FromDomain(DomainException ex) =>
           new InnerError(ex.Title, ex.Message, ex.StatusCode);
    }

}

