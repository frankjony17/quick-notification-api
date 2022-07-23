using FluentValidation;
using FluentValidation.Results;
using Quick.Notification.Domain.Configurations.Commands;
using Quick.Notification.Domain.Notify.Commands.Responses;
using Quick.Notification.Domain.Notify.Model;
using Quick.Notification.Domain.Notify.Validations;
using System;

namespace Quick.Notification.Domain.Notify.Commands.Requests
{
    public class NotifyCommandRequest : Command<NotifyCommandResponse>
    {

        private readonly IValidator<NotifyRequest> _validator;
        private ValidationResult _validationResult;

        public NotifyCommandRequest(NotifyRequest notifyRequest)
        {
            _validator = new NotifyRequestValidator();
            NotifyRequest = notifyRequest;
        }

        public NotifyRequest NotifyRequest { get; private set; }

        public override ValidationResult ValidationResult
        {
            get
            {
                if (_validationResult is null)
                {
                    _validationResult = _validator.Validate(NotifyRequest);
                }
                return _validationResult;
            }
        }

    }
}
