namespace Shop.Shared.Dtos;

public class PhotoDto : BaseDto
{
    public string Url { get; set; } = string.Empty;

    public bool IsMain { get; set; }
}