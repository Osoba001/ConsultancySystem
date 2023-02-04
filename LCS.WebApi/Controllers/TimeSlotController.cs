using LCS.Application.Messages;
using LCS.Application.Queries.LanguageQ;
using LCS.Application.Queries.TimeSlotQ;
using LCS.Domain.Models;
using LCS.WebApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisService _redis;
        private const string SLOTID = "timeslots";
        public TimeSlotController(IMediator mediator, IRedisService redis)
        {
            _mediator = mediator;
            _redis = redis;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var slots= await _redis.GetRecordAsync<List<TimeSlotResponse>>(SLOTID);
            slots ??= await LoadFromDb();
            return Ok(slots);
        }
        private async Task<List<TimeSlotResponse>> LoadFromDb()
        {
            List<TimeSlotResponse>? slots = new List<TimeSlotResponse>();
            var slotdb = await _mediator.SendQueryAsync<TimeSlotQueryHandler, TimeSlotQuery, List<TimeSlot>>(new TimeSlotQuery());

            foreach (var item in slotdb)
            {
                slots.Add(item);
            }
            await _redis.SetRecordAsync(SLOTID, slots, TimeSpan.FromDays(1));
            return slots;
        }
    }
}
