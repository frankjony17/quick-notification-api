using FluentValidation.Results;
using MediatR;
using System;

namespace Quick.Notification.Domain.Configurations.Commands
{
    public abstract class Command<TResponse> : ICommand, IRequest<TResponse>
    {
        protected Command()
        {
            MessageTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public long MessageTimestamp { get; private set; }
        public abstract ValidationResult ValidationResult { get; }
        public bool IsValid => ValidationResult.IsValid;
    }
}

