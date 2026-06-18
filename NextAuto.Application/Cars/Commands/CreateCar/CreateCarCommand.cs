using MediatR;
using NextAuto.Application.Cars.DTOs;
using NextAuto.Application.Common.ServiceResult;
using NextAuto.Application.Interfaces;
using NextAuto.Application.Interfaces.IServiceResult;
using NextAuto.Domain.Entities;

namespace NextAuto.Application.Cars.Commands.CreateCar;

public record CreateCarCommand(
    string Brand,
    string Model,
    int Year,
    int Mileage,
    double Price,
    string ImageUrl) : IRequest<IServiceResult<CarDto>>;

public class CreateCarCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCarCommand, IServiceResult<CarDto>>
{
    public async Task<IServiceResult<CarDto>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = new Car
        {
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year,
            Mileage = request.Mileage,
            Price = request.Price,
            ImageUrl = request.ImageUrl
        };

        unitOfWork.Cars.Add(car);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CarDto>.Success(car.ToDto());
    }
}
