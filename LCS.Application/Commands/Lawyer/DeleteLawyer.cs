using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record DeleteLawyer(Guid Id) : ICommand;

    public class DeleteLawyerHandle : ICommandHandler<DeleteLawyer>
    {
        public async Task<ActionResult> HandleAsync(DeleteLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.Delete(command.Id);
        }
    }
}
