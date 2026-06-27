using PrintManager.Domain.Entities;
using PrintManager.Domain.Enums;

namespace PrintManager.Application.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Pedido>> ListarAsync(StatusPedido? status, DateTime? periodoInicial, DateTime? periodoFinal, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Pedido>> ListarFinalizadosPorMesAsync(int mes, int ano, CancellationToken cancellationToken = default);
    Task<int> ContarPorStatusAsync(StatusPedido status, CancellationToken cancellationToken = default);
    Task<int> ContarTotalAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default);
}
