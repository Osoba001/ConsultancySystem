using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Language
{
    public record RemoveLanguage(Guid LanguageId) : ICommand;

    public class RemoveLanguageHandler : ICommandHandler<RemoveLanguage>
    {
        public async Task<ActionResult> HandleAsync(RemoveLanguage command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LanguageRepo.Delete(command.LanguageId);
        }
    }
}
