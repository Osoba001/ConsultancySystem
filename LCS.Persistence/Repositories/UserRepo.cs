using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;

namespace LCS.Persistence.Repositories
{
    public class UserRepo : BaseRepo<UserTB>, IUserRepo
    {
        public UserRepo(LCSDbContext context) : base(context)
        {
        }
    }
}
