using MediatR;
using NextAuto.Application.Clients.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Clients.Queries.GetClientById;

public record GetClientByIdQuery(int Id) : IRequest<IServiceResult<ClientDto>>;

public class GetClientByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetClientByIdQuery, IServiceResult<ClientDto>>
{
    public async Task<IServiceResult<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetByIdAsync(request.Id);

        if (client is null)
        {
            return ServiceResult<ClientDto>.Fail($"Клиент с Id {request.Id} не найден.", 404);
        }

        return ServiceResult<ClientDto>.Success(client.ToDto());
    }
}
