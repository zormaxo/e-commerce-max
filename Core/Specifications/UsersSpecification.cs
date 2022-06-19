using Core.Entities;

namespace Core.Specifications
{
    public class UsersSpecification : BaseSpecifcation<AppUser>
    {
        public UsersSpecification(string username)
          : base(x => x.UserName == username)
        {
        }
    }
}