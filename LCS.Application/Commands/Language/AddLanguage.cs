using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Language
{
    public record AddLanguage(string name) : ICommand;

    public record AddLanguageHandler : ICommandHandler<AddLanguage>
    {
        public async Task<ActionResult> HandleAsync(AddLanguage command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lang = await repo.LanguageRepo.FindOneByPredicate(x => x.Name.ToLower() == command.name.ToLower().Trim());
            if (lang == null)
                return await repo.LanguageRepo.Add(new LanguageTB() { Name = command.name });
            return repo.FailedAction("Record is already exixt.");
        }
    }
}
