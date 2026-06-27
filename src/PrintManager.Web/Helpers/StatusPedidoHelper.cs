using PrintManager.Domain.Enums;

namespace PrintManager.Web.Helpers;

public static class StatusPedidoHelper
{
    public static string ObterDescricao(StatusPedido status) => status switch
    {
        StatusPedido.Aguardando => "Aguardando",
        StatusPedido.EmImpressao => "Em Impressão",
        StatusPedido.Finalizado => "Finalizado",
        _ => status.ToString()
    };

    public static string ObterBadgeClass(StatusPedido status) => status switch
    {
        StatusPedido.Aguardando => "bg-warning text-dark",
        StatusPedido.EmImpressao => "bg-info text-dark",
        StatusPedido.Finalizado => "bg-success",
        _ => "bg-secondary"
    };
}
