using Law.Domain.Repositories;
using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand 
    {
       Task<ActionResult> HandleAsync(TCommand command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken=default);
    }

}   
