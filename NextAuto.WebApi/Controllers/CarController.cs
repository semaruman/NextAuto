using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextAuto.Application.Cars.Commands.CreateCar;
using NextAuto.Application.Cars.Commands.DeleteCar;
using NextAuto.Application.Cars.Commands.UpdateCar;
using NextAuto.Application.Cars.DTOs;
using NextAuto.Application.Cars.Queries.GetCarById;
using NextAuto.WebApi.Common;

namespace NextAuto.WebApi.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCarByIdQuery(id), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<CarDto>> Create(
        [FromBody] CreateCarRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateCarCommand(
                request.Brand,
                request.Model,
                request.Year,
                request.Mileage,
                request.Price,
                request.ImageUrl),
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CarDto>> Update(
        int id,
        [FromBody] UpdateCarRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateCarCommand(
                id,
                request.Brand,
                request.Model,
                request.Year,
                request.Mileage,
                request.Price,
                request.ImageUrl),
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCarCommand(id), cancellationToken);
        return result.ToActionResult();
    }
}

public record CreateCarRequest(
    string Brand,
    string Model,
    int Year,
    int Mileage,
    double Price,
    string ImageUrl);

public record UpdateCarRequest(
    string Brand,
    string Model,
    int Year,
    int Mileage,
    double Price,
    string ImageUrl);
