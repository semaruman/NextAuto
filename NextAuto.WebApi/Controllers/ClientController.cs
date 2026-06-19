using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextAuto.Application.Clients.Commands.CreateClient;
using NextAuto.Application.Clients.Commands.DeleteClient;
using NextAuto.Application.Clients.Commands.UpdateClient;
using NextAuto.Application.Clients.DTOs;
using NextAuto.Application.Clients.Queries.GetClientById;
using NextAuto.WebApi.Common;

namespace NextAuto.WebApi.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetClientByIdQuery(id), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(
        [FromBody] CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateClientCommand(
                request.CarBrand,
                request.CarModel,
                request.ImageUrl),
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClientDto>> Update(
        int id,
        [FromBody] UpdateClientRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateClientCommand(
                id,
                request.CarBrand,
                request.CarModel,
                request.ImageUrl),
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteClientCommand(id), cancellationToken);
        return result.ToActionResult();
    }
}

public record CreateClientRequest(string CarBrand, string CarModel, string ImageUrl);

public record UpdateClientRequest(string CarBrand, string CarModel, string ImageUrl);
