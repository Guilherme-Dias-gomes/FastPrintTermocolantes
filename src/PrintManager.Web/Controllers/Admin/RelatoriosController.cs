using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Services;
using PrintManager.Web.ViewModels;

namespace PrintManager.Web.Controllers.Admin;

[Authorize]
[Route("admin/relatorios")]
public class RelatoriosController : Controller
{
    private readonly RelatorioService _relatorioService;

    public RelatoriosController(RelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(RelatorioFiltroViewModel filtro, CancellationToken cancellationToken)
    {
        var relatorio = await _relatorioService.ObterRelatorioMensalAsync(filtro.Mes, filtro.Ano, cancellationToken);
        ViewBag.Filtro = filtro;
        return View(relatorio);
    }
}
