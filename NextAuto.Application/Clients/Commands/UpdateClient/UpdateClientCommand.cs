using MediatR;
using NextAuto.Application.Clients.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand(
    int Id,
    string CarBrand,
    string CarModel,
    string ImageUrl) : IRequest<IServiceResult<ClientDto>>;

public class UpdateClientCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateClientCommand, IServiceResult<ClientDto>>
{
    public async Task<IServiceResult<ClientDto>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetByIdAsync(request.Id);

        if (client is null)
        {
            return ServiceResult<ClientDto>.Fail($"Клиент с Id {request.Id} не найден.", 404);
        }

        client.CarBrand = request.CarBrand;
        client.CarModel = request.CarModel;
        client.ImageUrl = request.ImageUrl;

        unitOfWork.Clients.Update(client);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<ClientDto>.Success(client.ToDto());
    }
}
