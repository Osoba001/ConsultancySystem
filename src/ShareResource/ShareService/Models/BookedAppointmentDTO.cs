namespace ShareServices.Models
{
    public class BookedAppointmentDTO
    {
        public string Receiver { get; set; }
        public string ReceiverEmail { get; set; }
        public string Client { get; set; }
        public string ClientEmail { get; set; }
        public string AppointmentName { get; set; }
        public string? ReviewAddress { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
