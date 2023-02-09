using LCS.Domain.Constants;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Application.Validations;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Client
{
    public record UpdateClient(Guid Id, string FirstName, string? MiddleName, string LastName,
        string PhoneNo, DateTime DOB, Gender Gender) : ICommand
    {
        public ActionResult Validate()
        {
            var res=new ActionResult();
            if (!FirstName.StringMaxLength(50))
            {
                res.AddError("First Name is to long.");
            }
            if (!LastName.StringMaxLength(50))
            {
                res.AddError("Last Name is to long.");
            }
            if (!PhoneNo.PhoneNoValid())
            {
                res.AddError($"Phone number is not valid.");
            }
            if (!DOB.PastDate(DateTime.Now.AddYears(-150)))
            {
                res.AddError("Invalid date of birth.");
            }
            return res;
        }
    }

    public class UpdateClientHandler : ICommandHandler<UpdateClient>
    {
        
        private static void SetClientData(UpdateClient cmd, ClientTB tB)
        {
            tB.FirstName = cmd.FirstName;
            tB.LastName = cmd.LastName;
            tB.MiddleName= cmd.MiddleName;
            tB.PhoneNo = cmd.PhoneNo;
            tB.DOB = cmd.DOB;
            tB.Gender = cmd.Gender;
        }

        public async Task<ActionResult> HandleAsync(UpdateClient command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var client = await repo.ClientRepo.GetById(command.Id);
            if (client != null)
            {
                SetClientData(command, client);
                return await repo.ClientRepo.Update(client);
            }
            else
                return repo.FailedAction("Record not found!");
        }
    }
}
