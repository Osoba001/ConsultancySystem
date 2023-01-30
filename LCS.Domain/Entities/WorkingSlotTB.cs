namespace LCS.Domain.Entities
{
    public class WorkingSlotTB:EntityBase
    {
        public LawyerTB Lawyer { get; set; }
        public TimeSlotTB TimeSlot { get; set; }
    }
}
