using FluentValidation.Results;

namespace Quick.Notification.Domain.Configurations.Commands
{
    public interface ICommand
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}

