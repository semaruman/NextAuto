using NextAuto.Domain.Entities;

namespace NextAuto.Application.Clients.DTOs;

internal static class ClientMappingExtensions
{
    internal static ClientDto ToDto(this Client client) => new()
    {
        Id = client.Id,
        CarBrand = client.CarBrand,
        CarModel = client.CarModel,
        ImageUrl = client.ImageUrl
    };
}
