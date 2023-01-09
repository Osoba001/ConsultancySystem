using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class DepartmentTB:EntityBase
    {
        public DepartmentTB(string name)
        {
            Name = name;
        }
        public string? Description { get; set; }
        public string Name { get; set; }
    }
}
