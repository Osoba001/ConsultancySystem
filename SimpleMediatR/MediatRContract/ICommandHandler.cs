using LCS.Domain.Repositories;
using LCS.Domain.Response;

namespace SimpleMediatR.MediatRContract
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand 
    {
       Task<ActionResult> HandleAsync(TCommand command, IRepoWrapper repo, CancellationToken cancellationToken=default);
    }

}   
