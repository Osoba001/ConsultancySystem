using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class ClientTB:PersonTB
    {
        public ClientTB()
        {
            Appointments = new List<AppointmentTB>();
        }
        public List<AppointmentTB> Appointments { get; set; }
        public static implicit operator Client(ClientTB tb)
        {
            return new Client()
            {
                Id = tb.Id,
                FirstName = tb.FirstName,
                LastName = tb.LastName,
                MiddleName = tb.MiddleName,
                Email = tb.Email,
                PhoneNo = tb.PhoneNo,
                Gender = tb.Gender,
                DOB = tb.DOB,
                CreatedDate = tb.CreatedDate,
                Appointments = ConvertAptmt(tb.Appointments)
            };
        }
        private static List<Appointment> ConvertAptmt(List<AppointmentTB> listtb)
        {
            var aptmts=new List<Appointment>();
            foreach (AppointmentTB aptmt in listtb)
            {
                aptmts.Add(aptmt);
            }
            return aptmts;
        }

    }
}
