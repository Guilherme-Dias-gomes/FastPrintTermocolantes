namespace PrintManager.Application.DTOs;

public class CriarPedidoDto
{
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string DescricaoServico { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public IReadOnlyList<ArquivoUploadDto> Arquivos { get; set; } = Array.Empty<ArquivoUploadDto>();
}
