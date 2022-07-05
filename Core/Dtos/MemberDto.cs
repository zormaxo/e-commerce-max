namespace Core.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public string LogoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
    }
}