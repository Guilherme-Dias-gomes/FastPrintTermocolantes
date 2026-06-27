using PrintManager.Domain.Enums;

namespace PrintManager.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string DescricaoServico { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusPedido Status { get; set; } = StatusPedido.Aguardando;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataFinalizacao { get; set; }
    public string? ObservacaoInterna { get; set; }

    public ICollection<PedidoArquivo> Arquivos { get; set; } = new List<PedidoArquivo>();
}
