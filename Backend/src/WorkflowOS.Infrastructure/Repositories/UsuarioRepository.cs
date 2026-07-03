using Microsoft.EntityFrameworkCore;
using WorkflowOS.Application.Interfaces.Persistence;
using WorkflowOS.Domain.Entities;
using WorkflowOS.Infrastructure.Persistence;

namespace WorkflowOS.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly WorkflowOSDbContext _context;

    public UsuarioRepository(WorkflowOSDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }

    public async Task<bool> EmailExisteAsync(string email)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .AnyAsync(x => x.Email == email);
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Usuario?> ObterPorIdAsync(long id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}