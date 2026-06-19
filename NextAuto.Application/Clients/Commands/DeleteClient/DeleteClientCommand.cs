using MediatR;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Clients.Commands.DeleteClient;

public record DeleteClientCommand(int Id) : IRequest<IServiceResult<bool>>;

public class DeleteClientCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteClientCommand, IServiceResult<bool>>
{
    public async Task<IServiceResult<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetByIdAsync(request.Id);

        if (client is null)
        {
            return ServiceResult<bool>.Fail($"Клиент с Id {request.Id} не найден.", 404);
        }

        unitOfWork.Clients.Remove(client);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}
