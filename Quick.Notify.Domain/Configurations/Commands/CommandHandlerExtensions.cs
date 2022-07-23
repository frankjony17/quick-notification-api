using Quick.Notification.Domain.Configurations.Exceptions;

namespace Quick.Notification.Domain.Configurations.Commands
{
    public static class CommandHandlerExtensions
    {
        public static void Validate(this ICommand commandRequest)
        {
            if (commandRequest is null)
            {
                throw new GenericException($"The command \"{nameof(ICommand)}\" could not be null.");
            }

            if (commandRequest.IsValid)
            {
                return;
            }

            throw new ValidationException(commandRequest.ValidationResult);
        }
    }
}

