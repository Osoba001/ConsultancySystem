using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class DepartmentTB:EntityBase
    {
        public DepartmentTB(string name, string description)
        {
            Name = name;
            JoinedDepartments = new List<LawyerDepartmentTB>();
        }
       
        public string? Description { get; set; }
        public string Name { get; set; }

        public List<LawyerDepartmentTB> JoinedDepartments { get; set; }
        public static implicit operator Department(DepartmentTB tb)
        {
            return new Department() { Id=tb.Id, Name = tb.Name,Description=tb.Description, CreatedDate=tb.CreatedDate};
        }
       

    }
}
