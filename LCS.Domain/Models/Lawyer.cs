using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Lawyer:Person
    {
        public Lawyer(User user,Department department) : base(user)
        {
            Departments = new List<Department>();
            Appointments = new List<Appointment>();
        }

        public bool AcceptOnlineAppointment { get; set; }
        public bool AcceptOfflineAppointment { get;set; }
        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public List<Department> Departments { get; set; }
        public List<Appointment> Appointments { get; set; }
        public string? OfficeAddress { get; set; }
    }
}
