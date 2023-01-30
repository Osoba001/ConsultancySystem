using LCS.Domain.Entities;
using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface IDepartmentRepo: IBaseRepo<DepartmentTB>
    {
        List<Department> Convertlist(List<DepartmentTB> listTB);
    }
}
