﻿namespace Shop.Core.Dtos
{
    public class ShowcaseDto : BaseDto
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
    }
}