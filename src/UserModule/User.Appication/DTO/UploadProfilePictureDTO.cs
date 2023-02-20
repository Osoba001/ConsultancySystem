
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ActionResponse;

namespace User.Application.DTO
{
    public record UploadProfilePictureDTO(IFormFile File, Guid userId)
    {
        private readonly List<string> ImageExtensions = new() { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public ActionResult Validate()
        {
            var res = new ActionResult();
            var extension = "." + File.FileName.Split('.')[File.FileName.Split('.').Length - 1];
            if (!ImageExtensions.Contains(extension.ToUpper()))
                res.AddError("Not a valid image");
            return res;
           
        }
    }
    

}
