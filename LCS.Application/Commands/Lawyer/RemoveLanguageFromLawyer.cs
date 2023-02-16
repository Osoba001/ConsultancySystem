using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record RemoveLanguageFromLawyer(Guid LawyerId, string Language) : ICommand
    {
        public ActionResult Validate() => new();
    }

    public record RemoveLanguageFromLawyerHandler : ICommandHandler<RemoveLanguageFromLawyer>
    {
        public async Task<ActionResult> HandleAsync(RemoveLanguageFromLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer != null)
            {
                lawyer.Languages.Remove(command.Language);
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found.");
        }
    }
}
