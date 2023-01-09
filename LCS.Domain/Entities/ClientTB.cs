using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class ClientTB:PersonTB
    {
        public ClientTB(UserTB user) : base(user)
        {
            Appointments = new List<AppointmentTB>();
        }

        public List<AppointmentTB> Appointments { get; set; }
    }
}
