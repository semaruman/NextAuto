using System.ComponentModel.DataAnnotations;

namespace NextAuto.AdminPanelMvcApp.Models;

public class ClientFormViewModel
{
    [Required(ErrorMessage = "Укажите марку автомобиля")]
    [Display(Name = "Марка автомобиля")]
    public string CarBrand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите модель автомобиля")]
    [Display(Name = "Модель автомобиля")]
    public string CarModel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите URL изображения")]
    [Display(Name = "URL изображения")]
    [Url(ErrorMessage = "Укажите корректный URL")]
    public string ImageUrl { get; set; } = string.Empty;
}
