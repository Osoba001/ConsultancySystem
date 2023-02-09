using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record AddLanguageToLawyer(Guid LawyerId, Guid LanguageId) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public class AddLanguageToLawyerHandler : ICommandHandler<AddLanguageToLawyer>
    {
        public async Task<ActionResult> HandleAsync(AddLanguageToLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.AddLanguage(command.LawyerId,command.LanguageId);
        }
    }
}
