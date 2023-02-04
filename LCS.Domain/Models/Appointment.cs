using LCS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Appointment: ModelBase
    {
        public Appointment()
        {

        }
        public Appointment(Lawyer lawyer, Client client, DateTime reviewDate, string timeSlot, double charge, 
            string appointmentType, string description,string language, Star star)
        {
            Lawyer = lawyer;
            Client = client;
            AppointmentType = appointmentType;
            ReviewDate = reviewDate;
            Charge = charge;
            TimeSlot= timeSlot;
            CaseDescription = description;
            Language = language;
            Stars = star;
        }
       
        public Lawyer Lawyer { get; set; }
        public Client Client { get; set; }
        public string TimeSlot { get; set; }
        public string Language { get; set; }
        public string CaseDescription { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool HasReviewed { get; set; }
        public bool IsCancel { get; set; }
        public double Charge { get; set; }
        public string AppointmentType { get; set; }
        public string? LawyerReport { get; set; }
        public string? ClientFeedBack { get; set; }
        public Star? Stars { get; set; }
    }
}
