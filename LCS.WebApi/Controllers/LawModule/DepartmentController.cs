using Law.Application.Commands.DepartmentC;
using Law.Application.Queries.Department;
using Law.Application.Response;
using Microsoft.AspNetCore.Mvc;
using ShareServices.AsDatabase;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : CustomControllerBase
    {
        private const string DepartmentsRedisChinnelId = "departments";
        public DepartmentController(IMediator mediator) : base(mediator) { }
        public DepartmentController(IMediator mediator, IRedisDatabase redisDatabase) : base(mediator, redisDatabase) { }


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
            return await ExecuteAsync<DeleteDepartmentHandler,DeleteDepartment>(new DeleteDepartment(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await QueryAsync<AllDepartmentHandler,AllDepartmentQuery,List<DepartmentResponse>>(new AllDepartmentQuery(),DepartmentsRedisChinnelId);
        }
        [HttpGet("byId")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await QueryAsync<DepartmentByIdHandler,DepartmentByIdQuery>(new DepartmentByIdQuery(id));
        }
    }
}
