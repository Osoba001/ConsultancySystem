using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Repositories
{
    public class LawyerRepo : BaseRepo<LawyerTB>, ILawyerRepo
    {
        public LawyerRepo(LCSDbContext context) : base(context)
        {
        }
    }
}
