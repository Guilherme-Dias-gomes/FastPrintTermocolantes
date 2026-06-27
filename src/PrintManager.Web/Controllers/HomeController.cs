using Microsoft.AspNetCore.Mvc;

namespace PrintManager.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Sobre() => View();

    public IActionResult Contato() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
