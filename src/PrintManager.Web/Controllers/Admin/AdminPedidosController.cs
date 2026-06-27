using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.DTOs;
using PrintManager.Application.Services;
using PrintManager.Web.ViewModels;

namespace PrintManager.Web.Controllers.Admin;

[Authorize]
[Route("admin/pedidos")]
public class AdminPedidosController : Controller
{
    private readonly PedidoService _pedidoService;

    public AdminPedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(PedidoFiltroViewModel filtro, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoService.ListarAsync(
            filtro.Status,
            filtro.PeriodoInicial,
            filtro.PeriodoFinal,
            cancellationToken);

        ViewBag.Filtro = filtro;
        return View(pedidos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Detalhes(Guid id, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoService.ObterDetalheAsync(id, cancellationToken);
        if (pedido is null)
            return NotFound();

        var model = new PedidoDetalheViewModel
        {
            Id = pedido.Id,
            NomeCliente = pedido.NomeCliente,
            Telefone = pedido.Telefone,
            DescricaoServico = pedido.DescricaoServico,
            Quantidade = pedido.Quantidade,
            ValorPago = pedido.ValorPago,
            Status = pedido.Status,
            DataCriacao = pedido.DataCriacao,
            DataFinalizacao = pedido.DataFinalizacao,
            ObservacaoInterna = pedido.ObservacaoInterna,
            WhatsAppUrl = pedido.WhatsAppUrl,
            Arquivos = pedido.Arquivos.Select(a => new PedidoArquivoViewModel
            {
                NomeArquivo = a.NomeArquivo,
                CaminhoArquivo = a.CaminhoArquivo
            }).ToList(),
            StatusEdicao = pedido.Status,
            ValorPagoEdicao = pedido.ValorPago,
            ObservacaoInternaEdicao = pedido.ObservacaoInterna
        };

        return View(model);
    }

    [HttpPost("{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(Guid id, PedidoDetalheViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var pedido = await _pedidoService.ObterDetalheAsync(id, cancellationToken);
            if (pedido is null)
                return NotFound();

            model.NomeCliente = pedido.NomeCliente;
            model.Telefone = pedido.Telefone;
            model.DescricaoServico = pedido.DescricaoServico;
            model.Quantidade = pedido.Quantidade;
            model.DataCriacao = pedido.DataCriacao;
            model.DataFinalizacao = pedido.DataFinalizacao;
            model.WhatsAppUrl = pedido.WhatsAppUrl;
            model.Arquivos = pedido.Arquivos.Select(a => new PedidoArquivoViewModel
            {
                NomeArquivo = a.NomeArquivo,
                CaminhoArquivo = a.CaminhoArquivo
            }).ToList();

            return View("Detalhes", model);
        }

        var dto = new AtualizarPedidoDto
        {
            Status = model.StatusEdicao,
            ValorPago = model.ValorPagoEdicao,
            ObservacaoInterna = model.ObservacaoInternaEdicao,
            Finalizar = model.Finalizar
        };

        var atualizado = await _pedidoService.AtualizarPedidoAsync(id, dto, cancellationToken);
        if (!atualizado)
            return NotFound();

        TempData["Sucesso"] = "Pedido atualizado com sucesso.";
        return RedirectToAction(nameof(Detalhes), new { id });
    }
}
