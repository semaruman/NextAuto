using MediatR;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Cars.Commands.DeleteCar;

public record DeleteCarCommand(int Id) : IRequest<IServiceResult<bool>>;

public class DeleteCarCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCarCommand, IServiceResult<bool>>
{
    public async Task<IServiceResult<bool>> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Cars.GetByIdAsync(request.Id);

        if (car is null)
        {
            return ServiceResult<bool>.Fail($"Автомобиль с Id {request.Id} не найден.", 404);
        }

        unitOfWork.Cars.Remove(car);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}
