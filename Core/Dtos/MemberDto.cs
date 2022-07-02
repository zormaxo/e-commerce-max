namespace Core.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public string LgogUrl { get; set; }
        public DateTime LastActive { get; set; }
    }
}