using PrintManager.Application.DTOs;
using PrintManager.Application.Interfaces;
using PrintManager.Domain.Enums;

namespace PrintManager.Application.Services;

public class DashboardService
{
    private readonly IPedidoRepository _pedidoRepository;

    public DashboardService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<DashboardDto> ObterResumoAsync(CancellationToken cancellationToken = default)
    {
        return new DashboardDto
        {
            PedidosAguardando = await _pedidoRepository.ContarPorStatusAsync(StatusPedido.Aguardando, cancellationToken),
            PedidosEmImpressao = await _pedidoRepository.ContarPorStatusAsync(StatusPedido.EmImpressao, cancellationToken),
            PedidosFinalizados = await _pedidoRepository.ContarPorStatusAsync(StatusPedido.Finalizado, cancellationToken),
            TotalPedidos = await _pedidoRepository.ContarTotalAsync(cancellationToken)
        };
    }
}
