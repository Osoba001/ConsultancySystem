using Auth.Data;
using Auth.Entities;

namespace Auth.Repository
{
    public class UserRoleRepo : BaseRepo<UserRoleTb>, IUserRoleRepo
    {
        public UserRoleRepo(AuthDbContext context) : base(context)
        {
        }
    }
}
