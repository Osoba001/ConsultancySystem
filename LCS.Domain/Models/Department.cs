using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Department: ModelBase
    {
        public Department()
        {
            Lawyers = new();
        }
        public Department(string name, string description)
        {
            Name = name;
            Description = description;
            Lawyers=new();
        }
        public string? Description { get; set; }
        public string Name { get; set; }
        public List<Lawyer> Lawyers { get; set; }

    }
}
