using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Lawyer:Person
    {
        public Lawyer() 
        {
            Departments = new ();
            Appointments = new();
            Languages= new();
            WorkingSlots = new();
        }

        public bool AcceptOnlineAppointment { get; set; }
        public bool AcceptOfflineAppointment { get;set; }
        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public bool IsVerify { get; set; }
        public string? OfficeEmail { get; set; }
        public string Title { get; set; }
        public List<Department> Departments { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Language> Languages { get; set; }
        public List<TimeSlot> WorkingSlots { get; set; }
        public string? OfficeAddress { get; set; }
    }
}
