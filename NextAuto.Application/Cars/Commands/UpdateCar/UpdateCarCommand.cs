using MediatR;
using NextAuto.Application.Cars.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.Application.Cars.Commands.UpdateCar;

public record UpdateCarCommand(
    int Id,
    string Brand,
    string Model,
    int Year,
    int Mileage,
    double Price,
    string ImageUrl) : IRequest<IServiceResult<CarDto>>;

public class UpdateCarCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCarCommand, IServiceResult<CarDto>>
{
    public async Task<IServiceResult<CarDto>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Cars.GetByIdAsync(request.Id);

        if (car is null)
        {
            return ServiceResult<CarDto>.Fail($"Автомобиль с Id {request.Id} не найден.", 404);
        }

        car.Brand = request.Brand;
        car.Model = request.Model;
        car.Year = request.Year;
        car.Mileage = request.Mileage;
        car.Price = request.Price;
        car.ImageUrl = request.ImageUrl;

        unitOfWork.Cars.Update(car);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CarDto>.Success(car.ToDto());
    }
}
