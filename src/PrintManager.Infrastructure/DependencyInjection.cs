using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Services;
using PrintManager.Domain.Entities;
using PrintManager.Infrastructure.Data;
using PrintManager.Infrastructure.Repositories;
using PrintManager.Infrastructure.Services;

namespace PrintManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        services.AddScoped<PedidoService>();
        services.AddScoped<DashboardService>();
        services.AddScoped<RelatorioService>();
        services.AddScoped<AuthService>();

        return services;
    }

    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

        await context.Database.MigrateAsync();

        if (!await usuarioRepository.ExisteAlgumUsuarioAsync())
        {
            await usuarioRepository.AdicionarAsync(new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Administrador",
                Login = "admin",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123")
            });
        }
    }
}
