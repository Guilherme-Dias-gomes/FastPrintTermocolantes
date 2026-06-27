using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Services;

namespace PrintManager.Web.Controllers.Admin;

[Authorize]
[Route("admin")]
public class DashboardController : Controller
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard(CancellationToken cancellationToken)
    {
        var resumo = await _dashboardService.ObterResumoAsync(cancellationToken);
        return View(resumo);
    }
}
