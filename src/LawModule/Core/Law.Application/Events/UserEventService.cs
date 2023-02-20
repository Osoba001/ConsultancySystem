using Law.Domain.Models;
using Law.Domain.Repositories;
using ShareServices.Constant;
using ShareServices.Events;
using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Law.Application.EventService
{
    public class UserEventService: ILawModuleEventService
    {
        private readonly IRepoWrapper _repo;

        public UserEventService(IRepoWrapper repo)
        {
            _repo = repo;
        }

        public async void CreatedHandler(object? sender, CreatedUserArgs e)
        {
            if (e.Role==Role.Client)
            {
                var client = new Client() { Id = e.Id, Email = e.Email, FirstName = e.FirstName };
                var res = await _repo.ClientRepo.Add(client);
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }else if(e.Role == Role.Lawyer)
            {
                var client = new Lawyer() { Id = e.Id, Email = e.Email, FirstName = e.FirstName };
                var res = await _repo.LawyerRepo.Add(client);
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
            
        }

        public async void FalseDeletedHandler(object? sender, UserIdArgs e)
        {
            if (e.Role == Role.Client)
            {

                var user = await _repo.ClientRepo.GetById(e.Id);
                if (user != null)
                {
                    user.IsDelete = true;
                    var res = await _repo.ClientRepo.Update(user);
                    if (!res.IsSuccess)
                        throw new Exception(res.Errors());
                }
                else
                    throw new Exception("Client you're trying to delete is not found.");
            }
            else if (e.Role == Role.Lawyer)
            {
                var user = await _repo.LawyerRepo.GetById(e.Id);
                if (user != null)
                {
                    user.IsDelete = true;
                    var res = await _repo.LawyerRepo.Update(user);
                    if (!res.IsSuccess)
                        throw new Exception(res.Errors());
                }
                else
                    throw new Exception("user you're trying to delete is not found.");
            }
        }

        public async void HardDeletedHandler(object? sender, UserIdArgs e)
        {
            if (e.Role == Role.Client)
            {
                var res = await _repo.ClientRepo.Delete(e.Id);
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
            else if (e.Role == Role.Lawyer)
            {
                var res = await _repo.LawyerRepo.Delete(e.Id);
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
        }

        public async void UndoFalsedHandler(object? sender, UserIdArgs e)
        {
            if (e.Role == Role.Client)
            {
                var user = await _repo.ClientRepo.GetById(e.Id);
                if (user != null)
                {
                    user.IsDelete = false;
                    var res = await _repo.ClientRepo.Update(user);
                    if (!res.IsSuccess)
                        throw new Exception(res.Errors());
                }
                else
                    throw new Exception("Client you're trying to recover is not found.");
            }
            else if (e.Role == Role.Lawyer)
            {
                var user = await _repo.LawyerRepo.GetById(e.Id);
                if (user != null)
                {
                    user.IsDelete = false;
                    var res = await _repo.LawyerRepo.Update(user);
                    if (!res.IsSuccess)
                        throw new Exception(res.Errors());
                }
                else
                    throw new Exception("user you're trying to recover is not found.");
            }
        }
    }
}
