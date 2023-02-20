using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record AddLanguageToLawyer: ICommand
    {
        public Guid LawyerId { get; set; }
        public string Language { get; set; }
        public ActionResult Validate() => new();
    }


    public class AddLanguageToLawyerHandler : ICommandHandler<AddLanguageToLawyer>
    {
        public async Task<ActionResult> HandleAsync(AddLanguageToLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer != null)
            {
                lawyer.Languages.Add(command.Language);
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found.");

        }
    }
}
