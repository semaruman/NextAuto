using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextAuto.AdminPanelMvcApp.Models;
using NextAuto.Application.Cars.Commands.CreateCar;

namespace NextAuto.AdminPanelMvcApp.Controllers;

[Authorize]
public class CarController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CarFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CarFormViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var result = await mediator.Send(
            new CreateCarCommand(
                viewModel.Brand,
                viewModel.Model,
                viewModel.Year,
                viewModel.Mileage,
                viewModel.Price,
                viewModel.ImageUrl),
            cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Не удалось добавить автомобиль");
            return View(viewModel);
        }

        TempData["SuccessMessage"] = $"Автомобиль {result.Value!.Brand} {result.Value.Model} успешно добавлен (Id: {result.Value.Id}).";
        return RedirectToAction(nameof(Index));
    }
}
