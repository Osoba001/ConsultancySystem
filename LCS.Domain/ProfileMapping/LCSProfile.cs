using AutoMapper;
using LCS.Domain.Entities;
using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.ProfileMapping
{
    public class LCSProfile:Profile
    {
        public LCSProfile()
        {
            CreateMap<AppointmentTB, Appointment>();
            CreateMap<ClientTB, Client>();
            CreateMap<DepartmentTB, Department>();
            CreateMap<LawyerTB, Lawyer>();
        }
    }
}
