using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.DTOs;
using PrintManager.Application.Services;
using PrintManager.Web.ViewModels;

namespace PrintManager.Web.Controllers;

public class PedidosController : Controller
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public IActionResult Solicitar()
    {
        return View(new SolicitarPedidoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Solicitar(SolicitarPedidoViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var arquivos = new List<ArquivoUploadDto>();

        if (model.Arquivos is { Count: > 0 })
        {
            foreach (var arquivo in model.Arquivos.Where(a => a.Length > 0))
            {
                arquivos.Add(new ArquivoUploadDto
                {
                    NomeArquivo = arquivo.FileName,
                    Conteudo = arquivo.OpenReadStream()
                });
            }
        }

        var dto = new CriarPedidoDto
        {
            NomeCliente = model.NomeCliente,
            Telefone = model.Telefone,
            DescricaoServico = model.DescricaoServico,
            Quantidade = model.Quantidade,
            Arquivos = arquivos
        };

        await _pedidoService.CriarPedidoAsync(dto, cancellationToken);

        TempData["Sucesso"] = "Pedido enviado com sucesso! Entraremos em contato em breve.";
        return RedirectToAction(nameof(Solicitar));
    }
}
