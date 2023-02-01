using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Basket;

public class BasketItemDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required]
    public string PictureUrl { get; set; } = string.Empty;
}