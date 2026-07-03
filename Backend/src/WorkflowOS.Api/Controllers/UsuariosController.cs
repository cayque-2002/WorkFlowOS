using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkflowOS.Application.Features.Usuarios.Commands.CriarUsuario;

[ApiController]
[Route("api/usuarios")]
public sealed class UsuariosController : ControllerBase
{
    private readonly ISender _sender;

    public UsuariosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(
        CriarUsuarioCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id },
            new { id });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterPorId(
        long id,
        CancellationToken cancellationToken)
    {
        // Query virá na próxima etapa
        return Ok();
    }
}