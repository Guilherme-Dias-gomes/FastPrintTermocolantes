using System.ComponentModel.DataAnnotations;

namespace PrintManager.Web.ViewModels;

public class SolicitarPedidoViewModel
{
    [Required(ErrorMessage = "Informe o nome do cliente.")]
    [Display(Name = "Nome do Cliente")]
    public string NomeCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone.")]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descreva o serviço.")]
    [Display(Name = "Descrição do Serviço")]
    public string DescricaoServico { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a quantidade.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    [Display(Name = "Quantidade")]
    public int Quantidade { get; set; } = 1;

    [Display(Name = "Arquivos")]
    public List<IFormFile>? Arquivos { get; set; }
}
