using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Client
{
    public record DeleteClient(Guid Id) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public class DeleteClientHandler : ICommandHandler<DeleteClient>
    {
        public async Task<ActionResult> HandleAsync(DeleteClient command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.ClientRepo.Delete(command.Id);
        }
    }
}
