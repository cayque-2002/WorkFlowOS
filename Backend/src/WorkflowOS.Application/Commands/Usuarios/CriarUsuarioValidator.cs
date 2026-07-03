using FluentValidation;

namespace WorkflowOS.Application.Features.Usuarios.Commands.CriarUsuario;

public sealed class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
{
    public CriarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Senha)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100);
    }
}