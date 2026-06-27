namespace PrintManager.Application.DTOs;

public class DashboardDto
{
    public int PedidosAguardando { get; set; }
    public int PedidosEmImpressao { get; set; }
    public int PedidosFinalizados { get; set; }
    public int TotalPedidos { get; set; }
}
