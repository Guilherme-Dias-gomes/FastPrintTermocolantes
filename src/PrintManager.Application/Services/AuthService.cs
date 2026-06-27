using PrintManager.Application.Interfaces;
using PrintManager.Domain.Entities;

namespace PrintManager.Application.Services;

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> ValidarLoginAsync(string login, string senha, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.ObterPorLoginAsync(login.Trim(), cancellationToken);
        if (usuario is null)
            return null;

        return BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash) ? usuario : null;
    }
}
