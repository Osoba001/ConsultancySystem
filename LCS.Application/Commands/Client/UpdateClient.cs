using LCS.Domain.Constants;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Client
{
    public record UpdateClient(Guid Id, string FistName, string MiddleName, string LastName,
        string PhoneNo, DateTime DOB, Gender Gender) : ICommand;

    public class UpdateClientHandler : ICommandHandler<UpdateClient>
    {
        
        private static void SetClientData(UpdateClient cmd, ClientTB tB)
        {
            tB.FirstName = cmd.FistName;
            tB.LastName = cmd.LastName;
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
