using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record VerifyLawyer(Guid Id) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
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
