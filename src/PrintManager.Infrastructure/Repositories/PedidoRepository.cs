using Microsoft.EntityFrameworkCore;
using PrintManager.Application.Interfaces;
using PrintManager.Domain.Entities;
using PrintManager.Domain.Enums;
using PrintManager.Infrastructure.Data;

namespace PrintManager.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .Include(p => p.Arquivos)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Pedido>> ListarAsync(
        StatusPedido? status,
        DateTime? periodoInicial,
        DateTime? periodoFinal,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Pedidos.AsQueryable();

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        if (periodoInicial.HasValue)
        {
            var inicio = periodoInicial.Value.Date.ToUniversalTime();
            query = query.Where(p => p.DataCriacao >= inicio);
        }

        if (periodoFinal.HasValue)
        {
            var fim = periodoFinal.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();
            query = query.Where(p => p.DataCriacao <= fim);
        }

        return await query
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Pedido>> ListarFinalizadosPorMesAsync(int mes, int ano, CancellationToken cancellationToken = default)
    {
        var inicio = new DateTime(ano, mes, 1, 0, 0, 0, DateTimeKind.Utc);
        var fim = inicio.AddMonths(1).AddTicks(-1);

        return await _context.Pedidos
            .Where(p => p.Status == StatusPedido.Finalizado
                && p.DataFinalizacao >= inicio
                && p.DataFinalizacao <= fim)
            .OrderBy(p => p.DataFinalizacao)
            .ToListAsync(cancellationToken);
    }

    public Task<int> ContarPorStatusAsync(StatusPedido status, CancellationToken cancellationToken = default)
    {
        return _context.Pedidos.CountAsync(p => p.Status == status, cancellationToken);
    }

    public Task<int> ContarTotalAsync(CancellationToken cancellationToken = default)
    {
        return _context.Pedidos.CountAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
