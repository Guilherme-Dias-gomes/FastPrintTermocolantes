using PrintManager.Domain.Enums;

namespace PrintManager.Application.DTOs;

public class PedidoArquivoDto
{
    public Guid Id { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
}

public class PedidoDetalheDto
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string DescricaoServico { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusPedido Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public string? ObservacaoInterna { get; set; }
    public IReadOnlyList<PedidoArquivoDto> Arquivos { get; set; } = Array.Empty<PedidoArquivoDto>();
    public string WhatsAppUrl { get; set; } = string.Empty;
}
