
using Law.Application.Queries.Lawyer;
using SimpleMediatR.MediatRContract;
using System.IO;
using Utilities.ActionResponse;

namespace ConsultancySystem.WebApi.Files.Manager
{
    public class FileManager : IFileManager
    {
        private readonly IMediator _mediator;

        public FileManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        private bool IsFileValid(IFormFile file)
        {
            List<string> achiveExtensions = new() { ".zip" };
            var extension = "." + file.FileName.Split('.')[^1];
            return achiveExtensions.Contains(extension.ToLower());
        }
        public async Task<ActionResult> SaveFile(IFormFile file, Guid ownnerId, string destinationPath = "Files\\Qualification")
        {
            var res = new ActionResult();
            var extension = "." + file.FileName.Split('.')[^1];
            if (IsFileValid(file))
            {
                var resp=await _mediator.QueryNullableAsync<LawyerByIdQueryHandler, LawyerByIdQuery> (new LawyerByIdQuery { Id = ownnerId });
                if (!resp.IsSuccess)
                    return resp;
                string fileName = ownnerId.ToString() + extension;
                var directory = Path.Join(Directory.GetCurrentDirectory(), destinationPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var filePath = Path.Join(Directory.GetCurrentDirectory(), destinationPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
                res.AddError("Invalid file format.Must be a zip file");
            return res;
        }
    }
}
