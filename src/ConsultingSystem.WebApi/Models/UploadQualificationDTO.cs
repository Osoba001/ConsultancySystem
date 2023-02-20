namespace ConsultancySystem.WebApi.Models
{
    public class UploadQualificationDTO
    {
        public IFormFile File { get; set; }
        public Guid UserId { get; set; }
    }
}
