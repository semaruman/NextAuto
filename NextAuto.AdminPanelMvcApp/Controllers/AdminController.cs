using Microsoft.AspNetCore.Mvc;

namespace NextAuto.AdminPanelMvcApp.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}