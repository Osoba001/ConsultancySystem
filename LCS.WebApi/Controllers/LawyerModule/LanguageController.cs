using LCS.Application.Messages;
using LCS.Application.Queries.LanguageQ;
using LCS.Domain.Models;
using LCS.WebApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisService _redis;
        private const string LANGUAGESID = "languages";

        public LanguageController(IMediator mediator, IRedisService redis)
        {
            _mediator = mediator;
            _redis = redis;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var langs = await _redis.GetRecordAsync<List<LanguageResponse>>(LANGUAGESID);
            langs ??= await LoadFromDb();
            return Ok(langs);
        }

        private async Task<List<LanguageResponse>> LoadFromDb()
        {
            List<LanguageResponse>? langs = new List<LanguageResponse>();
            var langtb = await _mediator.SendQueryAsync<AllLanguageHandler, AllLanguageQuery, List<Language>>(new AllLanguageQuery());

            foreach (var item in langtb)
            {
                langs.Add(item);
            }
            await _redis.SetRecordAsync(LANGUAGESID, langs, TimeSpan.FromDays(1));
            return langs;
        }
    }
}
