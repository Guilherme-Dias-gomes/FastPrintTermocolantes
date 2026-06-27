namespace PrintManager.Application.Services;

public static class WhatsAppHelper
{
    public static string GerarUrl(string telefone, string nomeCliente)
    {
        var telefoneLimpo = new string(telefone.Where(char.IsDigit).ToArray());
        if (telefoneLimpo.StartsWith("0"))
            telefoneLimpo = telefoneLimpo.TrimStart('0');

        var mensagem = Uri.EscapeDataString(
            $"Olá {nomeCliente}, estamos entrando em contato sobre seu pedido.");

        return $"https://wa.me/{telefoneLimpo}?text={mensagem}";
    }
}
