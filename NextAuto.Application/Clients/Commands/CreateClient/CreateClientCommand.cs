using MediatR;
using NextAuto.Application.Clients.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;
using NextAuto.Domain.Entities;

namespace NextAuto.Application.Clients.Commands.CreateClient;

public record CreateClientCommand(
    string CarBrand,
    string CarModel,
    string ImageUrl) : IRequest<IServiceResult<ClientDto>>;

public class CreateClientCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateClientCommand, IServiceResult<ClientDto>>
{
    public async Task<IServiceResult<ClientDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            CarBrand = request.CarBrand,
            CarModel = request.CarModel,
            ImageUrl = request.ImageUrl
        };

        unitOfWork.Clients.Add(client);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<ClientDto>.Success(client.ToDto());
    }
}
