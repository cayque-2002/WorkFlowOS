using MediatR;
using WorkflowOS.Application.Features.Usuarios.Commands.CriarUsuario;
using WorkflowOS.Application.Interfaces.Persistence;
using WorkflowOS.Domain.Entities;

public sealed class CriarUsuarioCommandHandler
    : IRequestHandler<CriarUsuarioCommand, long>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CriarUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<long> Handle(
        CriarUsuarioCommand request,
        CancellationToken cancellationToken)
    {
        var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(request.Email);

        if (usuarioExistente is not null)
        {
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");
        }

        var senhaHash = _passwordHasher.Hash(request.Senha);

        var usuario = Usuario.Criar(
            request.Nome,
            request.Email,
            senhaHash);

        await _usuarioRepository.AdicionarAsync(usuario);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return usuario.Id;
    }
}