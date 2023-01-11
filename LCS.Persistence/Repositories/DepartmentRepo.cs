using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;

namespace LCS.Persistence.Repositories
{
    public class DepartmentRepo : BaseRepo<DepartmentTB>, IDepartmentRepo
    {
        public DepartmentRepo(LCSDbContext context) : base(context)
        {
        }
    }
}
