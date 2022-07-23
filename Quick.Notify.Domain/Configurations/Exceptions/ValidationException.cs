using Quick.Notify.Domain.Configurations.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Quick.Notify.Domain.Configurations.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public sealed class ValidationException : DomainException
    {
        private readonly ValidationResult _validationResult;

        public ValidationException(ValidationResult validationResult)
           : base("validation error")
        {
            _validationResult = validationResult;
        }

        public IEnumerable<InnerError> GetErrors() =>
            _validationResult.Errors.Select(error => InnerError.FromValidation(error)).ToList();
    }
}

