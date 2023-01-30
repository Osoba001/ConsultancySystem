using LCS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Person: ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string FullName =>$"{FirstName} {MiddleName} {LastName}";
        public Gender? Gender { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }

        public int Age => ComputeAge();

        private int ComputeAge()
        {
            if (DOB!=null)
            {
                int age = DateTime.Now.Year - DOB.Value.Year;
                if (DateTime.Now.DayOfYear < DOB.Value.DayOfYear)
                {
                    age--;
                }
                return age;
            }
            return 0;
        }

    }
}
