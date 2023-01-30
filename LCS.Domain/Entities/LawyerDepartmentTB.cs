using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class LawyerDepartmentTB:EntityBase
    {
        public LawyerTB Lawyer { get; set; }
        public DepartmentTB Department { get; set; }

        //public static bool operator ==(LawyerDepartmentTB left, LawyerDepartmentTB right)
        //{
        //    return left.Id == right.Id;
        //}
        //public static bool operator !=(LawyerDepartmentTB left, LawyerDepartmentTB right)
        //{
        //    return !left.Equals(right);
        //}
    }
}
