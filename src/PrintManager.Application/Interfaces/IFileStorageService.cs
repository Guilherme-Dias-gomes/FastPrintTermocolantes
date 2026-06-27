namespace PrintManager.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> SalvarArquivoAsync(Stream conteudo, string nomeArquivo, CancellationToken cancellationToken = default);
    string ObterCaminhoFisico(string caminhoRelativo);
}
