using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Authorize]
    public class CustomControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> ExecuteAsync<TCommandHandler, TCommand>(TCommand command)
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
        
        public async Task<IActionResult> QueryAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : QueryHandler<TQuery>
        {
            return Ok(await _mediator.QueryAsync<TQueryHandler, TQuery>(query));

        }
        public async Task<IActionResult> QueryNullableAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : IQueryHandler<TQuery>
        {
            var res = await _mediator.QueryNullableAsync<TQueryHandler, TQuery>(query);
            if (res.IsSuccess)
                return Ok(res.data);
            return BadRequest(res.ToString());
            

        }
    }
}
