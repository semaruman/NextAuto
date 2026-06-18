using NextAuto.Domain.Entities;

namespace NextAuto.Application.Cars.DTOs;

internal static class CarMappingExtensions
{
    internal static CarDto ToDto(this Car car) => new()
    {
        Id = car.Id,
        Brand = car.Brand,
        Model = car.Model,
        Year = car.Year,
        Mileage = car.Mileage,
        Price = car.Price,
        ImageUrl = car.ImageUrl
    };
}
