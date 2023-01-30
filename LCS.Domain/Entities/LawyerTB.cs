using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class LawyerTB:PersonTB
    {
        public LawyerTB() 
        {
            JoinedDepartments = new();
            Appointments = new();
            Languages= new();
            WorkingSlot=new();
        }

        public bool AcceptOnlineAppointment { get; set; }
        public bool AcceptOfflineAppointment { get; set; }
        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public bool IsVerify { get; set; }
        public string? OfficeAddress { get; set; }
        public string? OfficeEmail { get; set; }
        public string Title { get; set; }
        public List<WorkingSlotTB> WorkingSlot { get; set; }
        public List<LawyerDepartmentTB> JoinedDepartments { get; set; }
        public List<AppointmentTB> Appointments { get; set; }
        public List<LawyerLanguageTB> Languages { get; set; }

        public static implicit operator Lawyer(LawyerTB lawyerTB)
        {

            var langs = lawyerTB.Languages.Select(x => new Language
            {
                Id = x.Id,
                Name = x.Language.Name,
                CreatedDate = x.CreatedDate
            }).ToList();
            var depts = lawyerTB.JoinedDepartments.Select(x => new Department
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Description = x.Department.Description,
                Name = x.Department.Name,
            }).ToList();

            var slots = lawyerTB.WorkingSlot.Select(x => new TimeSlot
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Index=x.TimeSlot.Index,
                StartHour=x.TimeSlot.StartHour,
                StartMinute=x.TimeSlot.StartMinute,
                EndHour=x.TimeSlot.EndHour,
                EndMinute=x.TimeSlot.EndMinute,
            }).ToList();
            return new Lawyer()
            {
                Id = lawyerTB.Id,
                Email = lawyerTB.Email,
                CreatedDate = lawyerTB.CreatedDate,
                FirstName = lawyerTB.FirstName,
                MiddleName = lawyerTB.MiddleName,
                LastName = lawyerTB.LastName,
                DOB = lawyerTB.DOB,
                PhoneNo = lawyerTB.PhoneNo,
                Gender = lawyerTB.Gender,
                AcceptOfflineAppointment = lawyerTB.AcceptOfflineAppointment,
                AcceptOnlineAppointment = lawyerTB.AcceptOnlineAppointment,
                OnlineCharge = lawyerTB.OnlineCharge,
                OfflineCharge = lawyerTB.OfflineCharge,
                OfficeAddress = lawyerTB.OfficeAddress,
                IsVerify = lawyerTB.IsVerify,
                Title = lawyerTB.Title,
                OfficeEmail = lawyerTB.OfficeEmail,
                Languages = langs,
                Departments = depts,
                WorkingSlots = slots,
                Appointments = ConverLangs(lawyerTB.Appointments)
            };
        }
        
        private static List<Appointment> ConverLangs(List<AppointmentTB> aptmtsTb)
        {
            List<Appointment> aptmts= new();
            foreach (var aptmt in aptmtsTb)
            {
                aptmts.Add(aptmt);
            }
            return aptmts;
        }

       
    }
}
