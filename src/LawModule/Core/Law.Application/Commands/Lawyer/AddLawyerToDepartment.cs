using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record AddLawyerToDepartment: ICommand
    {
        public Guid LawyerId { get; set; }
        public Guid DeptId { get; set; }
        public ActionResult Validate() => new();
    }


    public record AddLawyerToDepartmentHandler : ICommandHandler<AddLawyerToDepartment>
    {

        public async Task<ActionResult> HandleAsync(AddLawyerToDepartment command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(command.DeptId);
            if (dept != null)
            {
                var lawyer = dept.Lawyers.Where(x => x.Id == command.LawyerId).FirstOrDefault();
                if (lawyer == null)
                {
                    lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
                    if (lawyer != null)
                    {
                        dept.Lawyers.Add(lawyer);
                        return await repo.DepartmentRepo.Update(dept);
                    }
                    else
                        return repo.FailedAction("User is not found.");
                }
                else
                    return repo.FailedAction("User already exist in the department.");
            }
            else
                return repo.FailedAction("Department is not found!");
        }
    }
}
