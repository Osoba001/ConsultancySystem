using Microsoft.AspNetCore.Mvc;
using ShareServices.AsDatabase;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    public class CustomControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDatabase _redisDatabase;

        public CustomControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public CustomControllerBase(IMediator mediator, IRedisDatabase redisDatabase)
        {
            _mediator = mediator;
            _redisDatabase = redisDatabase;
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
        public async Task<IActionResult> ExecuteAsyncThrowExceptIfFail<TCommandHandler, TCommand>(TCommand command)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            var result = await _mediator.ExecuteCommandAsync<TCommandHandler, TCommand>(command);
            if (!result.IsSuccess)
                throw new Exception($"{result}");
            return Ok("Success");

        }
        public async Task<IActionResult> QueryAsync<TQueryHandler, TQuery, TResponse>(TQuery query, string RedisChinnelId)
            where TQuery: IQuery<TResponse>
            where TQueryHandler : IQueryHandler<TQuery, TResponse>
        {
            var res = await _redisDatabase.GetRecordAsync<TResponse>(RedisChinnelId);
            if (res != null)
                return Ok(res);
            TResponse response = await _mediator.QueryAsync<TQueryHandler, TQuery, TResponse>(query);
             await _redisDatabase.SetRecordAsync(RedisChinnelId, response);
            return Ok(response); 

        }
        public async Task<IActionResult> QueryAsync<TQueryHandler, TQuery, TResponse>(TQuery query)
            where TQuery : IQuery<TResponse>
            where TQueryHandler : IQueryHandler<TQuery, TResponse>
        {
            return Ok(await _mediator.QueryAsync<TQueryHandler, TQuery, TResponse>(query));

        }
        public async Task<IActionResult> QueryAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : IQueryHandler<TQuery>
        {
            var res = await _mediator.QueryAsync<TQueryHandler, TQuery>(query);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res.ToString());
            

        }
    }
}
