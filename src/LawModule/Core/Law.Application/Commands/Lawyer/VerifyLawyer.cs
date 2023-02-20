using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record VerifyLawyer : ICommand
    {
        public Guid Id { get; set; }
        public ActionResult Validate() => new();
    }

    public class VerifyLawyerHandler : ICommandHandler<VerifyLawyer>
    {
        public async Task<ActionResult> HandleAsync(VerifyLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res = await repo.LawyerRepo.GetById(command.Id);
            if (res != null)
            {
                if (res.IsVerify)
                {
                    return new ActionResult();
                }
                res.IsVerify = true;
                return await repo.LawyerRepo.Update(res);
            }
            else
                return repo.FailedAction("Record not found!");
        }
    }
}
