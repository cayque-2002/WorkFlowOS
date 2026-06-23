using Microsoft.EntityFrameworkCore;
using WorkflowOS.Domain.Entities;

namespace WorkflowOS.Infrastructure.Persistence;

public class WorkflowOSDbContext : DbContext
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
