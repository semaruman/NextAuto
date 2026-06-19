using Microsoft.AspNetCore.Mvc;

namespace NextAuto.AdminPanelMvcApp.Controllers;

[Route("admin/[controller]")]
public class CarController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}