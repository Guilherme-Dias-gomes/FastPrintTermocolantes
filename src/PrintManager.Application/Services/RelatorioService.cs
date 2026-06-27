using PrintManager.Application.DTOs;
using PrintManager.Application.Interfaces;

namespace PrintManager.Application.Services;

public class RelatorioService
{
    private readonly IPedidoRepository _pedidoRepository;

    public RelatorioService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<RelatorioMensalDto> ObterRelatorioMensalAsync(int mes, int ano, CancellationToken cancellationToken = default)
    {
        var pedidos = await _pedidoRepository.ListarFinalizadosPorMesAsync(mes, ano, cancellationToken);

        var itens = pedidos
            .Select(p => new RelatorioMensalItemDto
            {
                NomeCliente = p.NomeCliente,
                ValorPago = p.ValorPago,
                DataFinalizacao = p.DataFinalizacao,
                Quantidade = p.Quantidade
            })
            .ToList();

        return new RelatorioMensalDto
        {
            Mes = mes,
            Ano = ano,
            Itens = itens,
            QuantidadePedidosConcluidos = itens.Count,
            ValorTotalRecebido = itens.Sum(i => i.ValorPago ?? 0)
        };
    }
}
