using LCS.Application.Handlers.Appointment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : CustomControllerBase
    {
        public AppointmentController(IMediator mediator) : base(mediator) { }

        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointment appointment)
        {
            return await SendAsync<BookAppointmentHandler, BookAppointment>(appointment);
        }

        [HttpPost("client-feedback")]
        public async Task<IActionResult> ClientFeedback([FromBody] ClientFeedback feedback)
        {
            return await SendAsync<ClientFeedbackHandler, ClientFeedback>(feedback);
        }

        [HttpPost("review-appointment")]
        public async Task<IActionResult> ReviewAppointment([FromBody] ReviewAppointment reviewAppointment)
        {
            return await SendAsync<ReviewAppointmentHandler, ReviewAppointment>(reviewAppointment);
        }
    }
}
