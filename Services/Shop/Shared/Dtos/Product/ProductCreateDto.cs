﻿using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Product;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [RegularExpression(
        @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$",
        ErrorMessage = "Price must be a decimal (e.g 20.30)")]
    public decimal Price { get; set; }

    public string PictureUrl { get; set; } = string.Empty;

    [Required]
    public int ProductTypeId { get; set; }

    [Required]
    public int ProductBrandId { get; set; }
}