using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    public class CustomControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> SendAsync<TCommandHandler, TCommand>(TCommand command)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            var validate = command.Validate();
            if (!validate.IsSuccess)
                return BadRequest(validate.ToString());

            var result = await _mediator.ExecuteCommandAsync<TCommandHandler, TCommand>(command);
            if (!result.IsSuccess)
                return BadRequest(result.ToString());
            return Ok("Success");

        }
    }
}
