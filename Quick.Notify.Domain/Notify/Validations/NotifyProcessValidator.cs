using FluentValidation;
using Quick.Notify.Domain.Notify.Model;

namespace Quick.Notify.Domain.Notify.Validations
{
    public class NotifyProcessValidator : AbstractValidator<NotifyProcess>
    {
        public NotifyProcessValidator()
        {
            RuleFor(c => c.Mensagem.EndToendId)
                .NotNull()
                .NotEmpty()
                .WithMessage("EndToendId field is null or empty");

            RuleFor(c => c.Mensagem.ValorOperacao)
             .NotNull()
             .NotEmpty()
             .WithMessage("ValorOperacao field is null or empty");

            RuleFor(c => c.Mensagem.CpfCnpjUsuarioRecebedor)
            .NotNull()
            .NotEmpty()
            .WithMessage("CpfCnpjUsuarioRecebedor field is null or empty");

            RuleFor(c => c.Mensagem.StatusOperacao)
            .NotNull()
            .NotEmpty()
            .WithMessage("StatusOperacao field is null or empty");

            RuleFor(c => c.Mensagem.ReturnId)
                .Must(c => c.StartsWith('D'))
                .When(c => !string.IsNullOrEmpty(c.Mensagem.ReturnId))
                .WithMessage("ReturnId must start with D or field is null or empty");
        }
    }
}
