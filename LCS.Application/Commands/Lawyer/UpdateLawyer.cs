using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace Law.Application.Commands.Lawyer
{
    public record UpdateLawyer(Guid Id, string OfficeEmail,
        string PhoneNo, double OnlineCharge, double OfflineCharge, string Title) : ICommand
    {
        public ActionResult Validate()
        {
            var res = new ActionResult();

            if (!PhoneNo.PhoneNoValid())
                res.AddError($"Phone number is not valid.");
            if (OfficeEmail.EmailValid())
                res.AddError($"Invalid Email.");
            return res;
        }
    }

    public class UpdateLawyerHandler : ICommandHandler<UpdateLawyer>
    {
        public async Task<ActionResult> HandleAsync(UpdateLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res = await repo.LawyerRepo.GetById(command.Id);
            if (res != null)
            {
                SetLawyerData(command, res);
                return await repo.LawyerRepo.Update(res);
            }
            else
                return repo.FailedAction("Record not found!");
        }
        private static void SetLawyerData(UpdateLawyer cmd, Domain.Models.Lawyer tB)
        {
            tB.PhoneNo = cmd.PhoneNo;
            tB.OfflineCharge = cmd.OfflineCharge;
            tB.OnlineCharge = cmd.OnlineCharge;
            tB.Title = cmd.Title;
            tB.OfficeEmail = cmd.OfficeEmail;
        }


    }
}
