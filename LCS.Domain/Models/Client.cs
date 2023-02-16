using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Law.Domain.Models
{
    public class Client : Person
    {
        public Client()
        {
            Appointments = new List<Appointment>();
        }

        public List<Appointment> Appointments { get; set; }
    }
}
