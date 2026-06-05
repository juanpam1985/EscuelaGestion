using Microsoft.AspNetCore.Mvc;

namespace EscuelaGestion.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
