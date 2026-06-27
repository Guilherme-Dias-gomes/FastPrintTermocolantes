using System.ComponentModel.DataAnnotations;
using PrintManager.Domain.Enums;

namespace PrintManager.Web.ViewModels;

public class PedidoDetalheViewModel
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
    public string WhatsAppUrl { get; set; } = string.Empty;
    public IReadOnlyList<PedidoArquivoViewModel> Arquivos { get; set; } = Array.Empty<PedidoArquivoViewModel>();

    [Display(Name = "Status")]
    public StatusPedido StatusEdicao { get; set; }

    [Display(Name = "Valor Pago")]
    public decimal? ValorPagoEdicao { get; set; }

    [Display(Name = "Observação Interna")]
    public string? ObservacaoInternaEdicao { get; set; }

    public bool Finalizar { get; set; }
}

public class PedidoArquivoViewModel
{
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
}
