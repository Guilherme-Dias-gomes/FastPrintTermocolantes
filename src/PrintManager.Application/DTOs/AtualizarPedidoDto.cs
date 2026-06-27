using PrintManager.Domain.Enums;

namespace PrintManager.Application.DTOs;

public class AtualizarPedidoDto
{
    public StatusPedido Status { get; set; }
    public decimal? ValorPago { get; set; }
    public string? ObservacaoInterna { get; set; }
    public bool Finalizar { get; set; }
}
