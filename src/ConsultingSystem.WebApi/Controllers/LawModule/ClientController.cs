using Law.Application.Commands.ClientC;
using Law.Application.Queries.Client;
using Law.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : CustomControllerBase
    {

        public ClientController(IMediator mediator) : base(mediator) { }

        [HttpPost("book-online-appointment")]
        public async Task<IActionResult> BookOnlineAppointment([FromBody] BookOnlineAppointment appointment)
        {
            return await ExecuteAsync<BookOnlineAppointmentHandler, BookOnlineAppointment>(appointment);
        }

        [HttpPost("book-offline-appointment")]
        public async Task<IActionResult> BookOfflineAppointment([FromBody] BookOfflineAppointment appointment)
        {
            return await ExecuteAsync<BookOfflineAppointmentHandler, BookOfflineAppointment>(appointment);
        }

        [HttpPost("feedback")]
        public async Task<IActionResult> ClientFeedback([FromBody] ClientFeedback feedback)
        {
            return await ExecuteAsync<ClientFeedbackHandler, ClientFeedback>(feedback);
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            return await QueryNullableAsync<ClientByIdHandler,ClientByIdQuery>(new ClientByIdQuery {Id= id });
        }
        [HttpGet("appointment")]
        public async Task<IActionResult> GetAppointmentByClient(Guid ClientId)
        {
            return await QueryAsync<AppointmentByClientHandler,AppointmentByClientQuery>(new AppointmentByClientQuery { ClientId = ClientId });
        }
        [HttpGet("lawyers-for-online")]
        public async Task<IActionResult> LawyersForOnline(string language)
        {
            return await QueryAsync<LawyerForOnlineAppointmentHandler, LawyerForOnlineAppointment>(new LawyerForOnlineAppointment { Languge= language });
        }
        [HttpGet("lawyers-for-offline")]
        public async Task<IActionResult> LawyersForOffline(string language, string state, string location)
        {
            return await QueryAsync<LawyerForOfflineAppointmentHandler, LawyerForOfflineAppointment>(new LawyerForOfflineAppointment{Language= language,State= state,Location= location });
        }
    }
}
  