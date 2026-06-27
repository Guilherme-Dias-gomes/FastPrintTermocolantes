namespace PrintManager.Application.DTOs;

public class RelatorioMensalItemDto
{
    public string NomeCliente { get; set; } = string.Empty;
    public decimal? ValorPago { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public int Quantidade { get; set; }
}

public class RelatorioMensalDto
{
    public int Mes { get; set; }
    public int Ano { get; set; }
    public IReadOnlyList<RelatorioMensalItemDto> Itens { get; set; } = Array.Empty<RelatorioMensalItemDto>();
    public int QuantidadePedidosConcluidos { get; set; }
    public decimal ValorTotalRecebido { get; set; }
}
