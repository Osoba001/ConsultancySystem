using LCS.Application.Validations;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Department
{
    public record CreateDepartment(string Name, string Description) : ICommand
    {
        public ActionResult Validate()
        {
            var res=new ActionResult();
            if (!Name.StringMaxLength(100))
            {
                res.AddError("Department name is to long.");
            }
            if (!Description.StringMaxLength(200))
            {
                res.AddError("Department description is to long.");
            }
            return res;
        }
    }

    public class CreateDepartmentHandler : ICommandHandler<CreateDepartment>
    {
        public async Task<ActionResult> HandleAsync(CreateDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.FindOneByPredicate(x => x.Name.ToLower() == command.Name.Trim().ToLower());
            if (dept != null)
            {
                return repo.DepartmentRepo.FailedAction("Department already exist!");
            }
            return await repo.DepartmentRepo.Add(new DepartmentTB(command.Name.Trim(), command.Description));
        }
    }

}
