using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class LawyerTB:PersonTB
    {
        public LawyerTB(UserTB user, DepartmentTB department) : base(user)
        {
            Departments = new List<DepartmentTB>();
            Appointments = new List<AppointmentTB>();
        }

        public bool AcceptOnlineAppointment { get; set; }
        public bool AcceptOfflineAppointment { get; set; }
        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public List<DepartmentTB> Departments { get; set; }
        public List<AppointmentTB> Appointments { get; set; }
        public string? OfficeAddress { get; set; }
    }
}
