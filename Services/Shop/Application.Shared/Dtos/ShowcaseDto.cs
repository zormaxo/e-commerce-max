namespace Shop.Application.Shared.Dtos;

public class ShowcaseDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;
}