using LCS.Domain.Constants;
using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class AppointmentTB:EntityBase
    {
        public LawyerTB Lawyer { get; set; }
        public ClientTB Client { get; set; }
        public TimeSlotTB TimeSlot { get; set; }
        public LanguageTB language { get; set; }
        public string CaseDescription { get; set; }
        public DateTime ReviewDate { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public bool HasReviewed { get; set; }
        public bool IsCancel { get; set; }
        public double Charge { get; set; }

        public string LawyerReport { get; set; }
        public string ClientFeedBack { get; set; }
        public Star? Stars { get; set; }

        public static implicit operator Appointment(AppointmentTB tB)
        {
            return new Appointment()
            {
                Id = tB.Id,
                CaseDescription = tB.CaseDescription,
                Charge = tB.Charge,
                Client = tB.Client,
                ClientFeedBack = tB.ClientFeedBack,
                CreatedDate = tB.CreatedDate,
                TimeSlot = tB.TimeSlot,
                ReviewDate = tB.ReviewDate,
                AppointmentType = tB.AppointmentType,
                HasReviewed = tB.HasReviewed,
                IsCancel = tB.IsCancel,
                LawyerReport = tB.LawyerReport,
                Stars = tB.Stars,
                Lawyer = tB.Lawyer,
                language=tB.language
            };
        }

    }
}
