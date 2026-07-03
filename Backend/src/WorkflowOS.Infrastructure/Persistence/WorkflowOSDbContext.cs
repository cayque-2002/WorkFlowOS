using Microsoft.EntityFrameworkCore;
using WorkflowOS.Application.Interfaces.Persistence;
using WorkflowOS.Domain.Entities;

namespace WorkflowOS.Infrastructure.Persistence;

public class WorkflowOSDbContext : DbContext, IUnitOfWork
{
    public WorkflowOSDbContext(DbContextOptions<WorkflowOSDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new Configurations.UsuarioConfiguration());
    }
}