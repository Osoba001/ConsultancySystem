using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record RemoveLanguageFromLawyer(Guid JoinedLangId) : ICommand;

    public record RemoveLanguageFromLawyerHandler : ICommandHandler<RemoveLanguageFromLawyer>
    {
        public async Task<ActionResult> HandleAsync(RemoveLanguageFromLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.RemoveLanguage(command.JoinedLangId);
        }
    }
}
