using WorkflowOS.Domain.Entities;

namespace WorkflowOS.Application.Interfaces.Persistence;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(long id);

    Task<Usuario?> ObterPorEmailAsync(string email);

    Task<bool> EmailExisteAsync(string email);

    Task AdicionarAsync(Usuario usuario);

    Task AtualizarAsync(Usuario usuario);
}