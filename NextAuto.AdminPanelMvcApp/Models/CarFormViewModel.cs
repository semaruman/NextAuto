using System.ComponentModel.DataAnnotations;

namespace NextAuto.AdminPanelMvcApp.Models;

public class CarFormViewModel
{
    [Required(ErrorMessage = "Укажите марку")]
    [Display(Name = "Марка")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите модель")]
    [Display(Name = "Модель")]
    public string Model { get; set; } = string.Empty;

    [Required]
    [Range(1900, 2100, ErrorMessage = "Укажите корректный год")]
    [Display(Name = "Год выпуска")]
    public int Year { get; set; } = DateTime.Now.Year;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Пробег не может быть отрицательным")]
    [Display(Name = "Пробег (км)")]
    public int Mileage { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
    [Display(Name = "Цена")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Укажите URL изображения")]
    [Display(Name = "URL изображения")]
    [Url(ErrorMessage = "Укажите корректный URL")]
    public string ImageUrl { get; set; } = string.Empty;
}
