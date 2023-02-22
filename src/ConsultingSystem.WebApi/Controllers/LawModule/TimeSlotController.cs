using Law.Application.Commands.TimeSlot;
using Law.Application.Queries.TimeSlotQ;
using Microsoft.AspNetCore.Mvc;
using ShareServices.RedisService;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : CustomControllerBase
    {
        private const string SlotsRedisChanelId = "timeslots";

        public TimeSlotController(IMediator mediator,IRedisDatabase redisDatabase) : base(mediator,redisDatabase) { }

       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return await QueryAsync<TimeSlotQueryHandler, TimeSlotQuery>(new TimeSlotQuery(),SlotsRedisChanelId);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTimeSlots()
        {
            return await ExecuteAsync<CreateTimeSlotHandler, CreateTimeSlot>(new CreateTimeSlot());
        }
    }
}
