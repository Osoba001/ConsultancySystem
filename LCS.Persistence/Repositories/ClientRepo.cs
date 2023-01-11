using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;

namespace LCS.Persistence.Repositories
{
    public class ClientRepo : BaseRepo<ClientTB>, IClientRepo
    {
        public ClientRepo(LCSDbContext context) : base(context)
        {
        }
    }
}
