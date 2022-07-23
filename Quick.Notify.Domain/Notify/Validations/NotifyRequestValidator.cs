using FluentValidation;
using Quick.Notification.Domain.Notify.Model;

namespace Quick.Notification.Domain.Notify.Validations
{
    public class NotifyRequestValidator : AbstractValidator<NotifyRequest>
    {
        public NotifyRequestValidator()
        {
            RuleFor(c => c.Protocolo)
            .NotEmpty()
            .NotNull()
            .WithMessage("Protocolo field is null ou empty");

            RuleFor(c => c.Mensagem)
                .NotEmpty()
                .NotNull()
                .WithMessage("Message field is null ou empty");

        }
    }
}
