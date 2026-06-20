using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextAuto.AdminPanelMvcApp.Models;
using NextAuto.Application.Clients.Commands.CreateClient;

namespace NextAuto.AdminPanelMvcApp.Controllers;

[Authorize]
public class ClientController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ClientFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientFormViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var result = await mediator.Send(
            new CreateClientCommand(
                viewModel.CarBrand,
                viewModel.CarModel,
                viewModel.ImageUrl),
            cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Не удалось добавить клиента");
            return View(viewModel);
        }

        TempData["SuccessMessage"] = $"Клиент успешно добавлен (Id: {result.Value!.Id}, {result.Value.CarBrand} {result.Value.CarModel}).";
        return RedirectToAction(nameof(Index));
    }
}
