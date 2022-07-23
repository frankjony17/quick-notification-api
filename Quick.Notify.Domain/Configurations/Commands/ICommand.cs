using FluentValidation.Results;

namespace Quick.Notify.Domain.Configurations.Commands
{
    public interface ICommand
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}

