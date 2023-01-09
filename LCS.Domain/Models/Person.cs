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
        public Person(User user)
        {
            User = user;
        }
        public User User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName =>$"{FirstName} {LastName}";
        public Gender? Gender { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime DOB { get; set; }

        public int Age => ComputeAge();

        private int ComputeAge()
        {
            int age = DateTime.Now.Year - DOB.Year;
            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
            {
                age--;
            }
            return age;
        }

    }
}
