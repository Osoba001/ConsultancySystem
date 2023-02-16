﻿using Law.Domain.Repositories;
using ShareServices.Events;

namespace Law.Application.Commands.Lawyer
{
    public class UndoFalseDeleteLawyer
    {
        private readonly ILawyerEventService _lawyerEvent;
        private readonly IRepoWrapper _repo;
        public UndoFalseDeleteLawyer(ILawyerEventService lawyerEvent, IRepoWrapper repo)
        {
            _lawyerEvent = lawyerEvent;
            _repo = repo;
            _lawyerEvent.UndoFalseDeletedLawyer += UndoFalseDeleteLawyerHandle;
        }

        public async void UndoFalseDeleteLawyerHandle(object? sender, UserIdArgs e)
        {
            var user = await _repo.LawyerRepo.GetById(e.Id);
            if (user != null)
            {
                user.IsDelete = false;
                var res = await _repo.LawyerRepo.Update(user);
                _lawyerEvent.FalseDeletedLawyer -= UndoFalseDeleteLawyerHandle;
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
            else
                throw new Exception("user you're trying to recover is not found.");
        }
    }
}