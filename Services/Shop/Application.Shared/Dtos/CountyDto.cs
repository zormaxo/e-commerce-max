﻿namespace Shop.Application.Shared.Dtos;

public class CountyDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public CityDto City { get; set; }
}
