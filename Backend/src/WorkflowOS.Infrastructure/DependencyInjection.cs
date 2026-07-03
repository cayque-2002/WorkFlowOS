using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowOS.Application.Interfaces.Persistence;
using WorkflowOS.Infrastructure.Persistence;
using WorkflowOS.Infrastructure.Repositories;
using WorkflowOS.Infrastructure.Security;

namespace WorkflowOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WorkflowOSDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<WorkflowOSDbContext>());

        // Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // Security
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}