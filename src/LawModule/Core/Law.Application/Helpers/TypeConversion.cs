using Law.Application.Response;
using Law.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Law.Application.Helpers
{
    public static class TypeConversion
    {
        public static List<AppointmentResponse> AppointmentListConv(this List<Appointment> models)
        {
            List<AppointmentResponse> responses = new();
            foreach (var item in models)
            {
                responses.Add(item);
            }
            return responses;
        }
        public static List<ClientResponse> ClientListConv(this List<Client> models)
        {
            List<ClientResponse> responses = new();
            foreach (var item in models)
            {
                responses.Add(item);
            }
            return responses;
        }
        public static List<DepartmentResponse> DepartmentListConv(this List<Department> models)
        {
            List<DepartmentResponse> responses = new();
            foreach (var item in models)
            {
                responses.Add(item);
            }
            return responses;
        }
       
        public static List<LawyerResponse> LawyerListConv(this List<Lawyer> models)
        {
            List<LawyerResponse> responses = new();
            foreach (var item in models)
            {
                responses.Add(item);
            }
            return responses;
        }
        public static List<TimeSlotResponse> TimeSlotListConv(this List<TimeSlot> models)
        {
            List<TimeSlotResponse> responses = new();
            foreach (var item in models)
            {
                responses.Add(item);
            }
            return responses;
        }
    }
}
