using LCS.Application.Validations;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Language
{
    public record AddLanguage(string Name) : ICommand
    {
        public ActionResult Validate()
        {
            var res = new ActionResult();
            if (!Name.StringMaxLength(200))
            {
                res.AddError("Department description is to long.");
            }
            return res;
        }
    }

    public record AddLanguageHandler : ICommandHandler<AddLanguage>
    {
        public async Task<ActionResult> HandleAsync(AddLanguage command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lang = await repo.LanguageRepo.FindOneByPredicate(x => x.Name.ToLower() == command.Name.ToLower().Trim());
            if (lang == null)
                return await repo.LanguageRepo.Add(new LanguageTB() { Name = command.Name });
            return repo.FailedAction("Record is already exixt.");
        }
    }
}
