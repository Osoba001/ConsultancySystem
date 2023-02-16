using Law.Application.Response.Base;
using Law.Domain.Models;

namespace Law.Application.Response
{
    public class DepartmentResponse : BaseResponse
    {
        public string? Description { get; set; }
        public string Name { get; set; }

        public static implicit operator DepartmentResponse(Department model)
        {
            return new DepartmentResponse() { Id = model.Id, Name = model.Name, Description = model.Description, CreatedDate = model.CreatedDate };
        }
    }
}
