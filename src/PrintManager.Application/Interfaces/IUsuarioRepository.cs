using PrintManager.Domain.Entities;

namespace PrintManager.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<bool> ExisteAlgumUsuarioAsync(CancellationToken cancellationToken = default);
}
