using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace Law.Application.Commands.DepartmentC
{
    public record UpdateDepartment: ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ActionResult Validate()
        {
            var res = new ActionResult();
            if (!Description.StringMaxLength(200))
            {
                res.AddError("Department description is to long.");
            }
            return res;
        }
    }

    public class UpdateDepartmentHandler : ICommandHandler<UpdateDepartment>
    {
        public async Task<ActionResult> HandleAsync(UpdateDepartment command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(command.Id);
            if (dept is not null)
            {
                dept.Description = command.Description;
                return await repo.DepartmentRepo.Update(dept);
            }
            else
                return repo.DepartmentRepo.FailedAction("Record does not exist!");
        }
    }

}
