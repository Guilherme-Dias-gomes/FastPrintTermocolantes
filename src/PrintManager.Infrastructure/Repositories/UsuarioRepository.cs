using Microsoft.EntityFrameworkCore;
using PrintManager.Application.Interfaces;
using PrintManager.Domain.Entities;
using PrintManager.Infrastructure.Data;

namespace PrintManager.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
    }

    public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExisteAlgumUsuarioAsync(CancellationToken cancellationToken = default)
    {
        return _context.Usuarios.AnyAsync(cancellationToken);
    }
}
