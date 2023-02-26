using Law.Application.Commands.DepartmentC;
using Law.Application.Queries.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : CustomControllerBase
    {
        public DepartmentController(IMediator mediator) : base(mediator) { }


        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartment department)
        {
            return await ExecuteAsync<CreateDepartmentHandler,CreateDepartment>(department);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartment department)
        {
            return await ExecuteAsync<UpdateDepartmentHandler,UpdateDepartment>(department);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            return await ExecuteAsync<DeleteDepartmentHandler,DeleteDepartment>(new DeleteDepartment { Id=id});
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return await QueryAsync<AllDepartmentHandler,AllDepartmentQuery>(new AllDepartmentQuery());
        }
        [HttpGet("byId")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await QueryNullableAsync<DepartmentByIdHandler,DepartmentByIdQuery>(new DepartmentByIdQuery{ Id = id });
        }
    }
}
