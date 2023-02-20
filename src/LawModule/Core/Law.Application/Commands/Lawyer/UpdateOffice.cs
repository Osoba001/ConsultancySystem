using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record UpdateOffice : ICommand
    {
        public Guid LawyerId { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public ActionResult Validate() => new();
        
    }

    public class UpdateOfficeHandler : ICommandHandler<UpdateOffice>
    {
        public async Task<ActionResult> HandleAsync(UpdateOffice command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer= await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer != null)
            {
                lawyer.State = command.State;
                lawyer.Location = command.Location;
                lawyer.OfficeAddress = command.Address;

                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User not found");
        }
    }
}
