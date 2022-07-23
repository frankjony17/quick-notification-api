using FluentValidation;
using Quick.Notify.Domain.Notify.Model;

namespace Quick.Notify.Domain.Notify.Validations
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
