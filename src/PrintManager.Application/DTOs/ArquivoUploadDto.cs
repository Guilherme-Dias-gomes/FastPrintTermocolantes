namespace PrintManager.Application.DTOs;

public class ArquivoUploadDto
{
    public string NomeArquivo { get; set; } = string.Empty;
    public Stream Conteudo { get; set; } = Stream.Null;
}
