namespace PrintManager.Domain.Entities;

public class PedidoArquivo
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;

    public Pedido Pedido { get; set; } = null!;
}
