using System.ComponentModel.DataAnnotations;

namespace PrintManager.Web.ViewModels;

public class RelatorioFiltroViewModel
{
    [Required]
    [Range(1, 12)]
    [Display(Name = "Mês")]
    public int Mes { get; set; } = DateTime.Now.Month;

    [Required]
    [Range(2020, 2100)]
    [Display(Name = "Ano")]
    public int Ano { get; set; } = DateTime.Now.Year;
}
