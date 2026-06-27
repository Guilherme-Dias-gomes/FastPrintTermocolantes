using PrintManager.Domain.Enums;

namespace PrintManager.Application.DTOs;

public class PedidoListagemDto
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public StatusPedido Status { get; set; }
    public DateTime DataCriacao { get; set; }
}
