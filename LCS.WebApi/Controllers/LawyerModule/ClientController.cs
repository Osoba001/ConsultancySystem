using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : CustomControllerBase
    {
        public ClientController(IMediator mediator) : base(mediator) { }


    }
}
