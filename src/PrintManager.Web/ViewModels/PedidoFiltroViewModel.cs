using System.ComponentModel.DataAnnotations;
using PrintManager.Domain.Enums;

namespace PrintManager.Web.ViewModels;

public class PedidoFiltroViewModel
{
    [Display(Name = "Status")]
    public StatusPedido? Status { get; set; }

    [Display(Name = "Período Inicial")]
    [DataType(DataType.Date)]
    public DateTime? PeriodoInicial { get; set; }

    [Display(Name = "Período Final")]
    [DataType(DataType.Date)]
    public DateTime? PeriodoFinal { get; set; }
}
