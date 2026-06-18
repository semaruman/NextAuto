using MediatR;
using NextAuto.Application.Cars.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Cars.Queries.GetCarById;

public record GetCarByIdQuery(int Id) : IRequest<IServiceResult<CarDto>>;

public class GetCarByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCarByIdQuery, IServiceResult<CarDto>>
{
    public async Task<IServiceResult<CarDto>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Cars.GetByIdAsync(request.Id);

        if (car is null)
        {
            return ServiceResult<CarDto>.Fail($"Автомобиль с Id {request.Id} не найден.", 404);
        }

        return ServiceResult<CarDto>.Success(car.ToDto());
    }
}
