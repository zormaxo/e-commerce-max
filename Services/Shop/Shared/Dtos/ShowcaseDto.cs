﻿using Shop.Core.Shared.Dtos;

namespace Shop.Shared.Dtos;

public class ShowcaseDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;

    public decimal Price { get; set; }
}