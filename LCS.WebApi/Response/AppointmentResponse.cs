using LCS.Domain.Constants;
using LCS.Domain.Models;
using LCS.WebApi.Response.Base;

namespace LCS.WebApi.Response
{
    public class AppointmentResponse: BaseResponse
    {
        
        public LawyerResponse Lawyer { get; set; }
        public ClientResponse Client { get; set; }
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
        public static implicit operator AppointmentResponse(Appointment model)
        {
            return new AppointmentResponse()
            {
                Id = model.Id,
                CaseDescription = model.CaseDescription,
                Charge = model.Charge,
                Client = model.Client,
                ClientFeedBack = model.ClientFeedBack,
                CreatedDate = model.CreatedDate,
                TimeSlot = model.TimeSlot.ToString()!,
                ReviewDate = model.ReviewDate,
                AppointmentType = model.AppointmentType.ToString(),
                HasReviewed = model.HasReviewed,
                IsCancel = model.IsCancel,
                LawyerReport = model.LawyerReport,
                Stars = model.Stars,
                Lawyer = model.Lawyer,
                Language = model.Language
            };
        }
    }
}
