using Law.Application.Commands.TimeSlot;
using Law.Application.Queries.TimeSlotQ;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : CustomControllerBase
    {

        public TimeSlotController(IMediator mediator) : base(mediator) { }

       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return await QueryAsync<TimeSlotQueryHandler, TimeSlotQuery>(new TimeSlotQuery());
        }
        [HttpPost]
        public async Task<IActionResult> CreateTimeSlots()
        {
            return await ExecuteAsync<CreateTimeSlotHandler, CreateTimeSlot>(new CreateTimeSlot());
        }
    }
}
