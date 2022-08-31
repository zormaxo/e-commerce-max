using Application.Entities;

namespace Application.Specifications
{
    public class UsersSpecification : BaseSpecifcation<AppUser>
    {
        public UsersSpecification(string username)
          : base(x => x.Username == username)
        {
        }
    }
}