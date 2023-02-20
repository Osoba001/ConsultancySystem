using Utilities.ActionResponse;

namespace ConsultancySystem.WebApi.Files.Manager
{
    public interface IFileManager
    {
        Task<ActionResult> SaveFile(IFormFile file, Guid ownnerId, string destinationPath = "Files\\Qualification");
    }
}