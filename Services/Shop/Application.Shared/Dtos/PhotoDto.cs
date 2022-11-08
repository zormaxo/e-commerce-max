using Shop.Core.Dtos;

namespace Core.Dtos
{
    public class PhotoDto : BaseDto
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}