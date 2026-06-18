namespace NextAuto.Application.Cars.DTOs;

public class CarDto
{
    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Mileage { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}
