using Microsoft.AspNetCore.Hosting;
using PrintManager.Application.Interfaces;

namespace PrintManager.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;
    private const string UploadsFolder = "uploads";

    public FileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SalvarArquivoAsync(Stream conteudo, string nomeArquivo, CancellationToken cancellationToken = default)
    {
        var agora = DateTime.Now;
        var pastaRelativa = Path.Combine(UploadsFolder, agora.Year.ToString(), agora.Month.ToString("D2"));
        var pastaFisica = Path.Combine(_environment.WebRootPath, pastaRelativa);

        Directory.CreateDirectory(pastaFisica);

        var nomeUnico = $"{Guid.NewGuid()}_{Path.GetFileName(nomeArquivo)}";
        var caminhoFisico = Path.Combine(pastaFisica, nomeUnico);

        await using var fileStream = new FileStream(caminhoFisico, FileMode.Create);
        await conteudo.CopyToAsync(fileStream, cancellationToken);

        return Path.Combine(pastaRelativa, nomeUnico).Replace('\\', '/');
    }

    public string ObterCaminhoFisico(string caminhoRelativo)
    {
        return Path.Combine(_environment.WebRootPath, caminhoRelativo.Replace('/', Path.DirectorySeparatorChar));
    }
}
