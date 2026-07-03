using MediatR;

namespace WorkflowOS.Application.Features.Usuarios.Commands.CriarUsuario;

public sealed record CriarUsuarioCommand(
    string Nome,
    string Email,
    string Senha
) : IRequest<long>;