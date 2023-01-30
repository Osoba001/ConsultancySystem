﻿using LCS.Domain.Constants;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record UpdateLawyer(Guid Id, string FistName, string MiddleName, string LastName, string? OfficeEmail,
        string PhoneNo, DateTime DOB, Gender Gender, string? OfficeAddress, bool AcceptOnlineAppointment,
        bool AcceptOfflineAppointment, double OnlineCharge, double OfflineCharge, string Title) : ICommand;


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
        private static void SetLawyerData(UpdateLawyer cmd, LawyerTB tB)
        {
            tB.FirstName = cmd.FistName;
            tB.LastName = cmd.LastName;
            tB.PhoneNo = cmd.PhoneNo;
            tB.DOB = cmd.DOB;
            tB.Gender = cmd.Gender;
            tB.OfficeAddress = cmd.OfficeAddress;
            tB.MiddleName = cmd.MiddleName;
            tB.OfflineCharge = cmd.OfflineCharge;
            tB.OnlineCharge = cmd.OnlineCharge;
            tB.AcceptOfflineAppointment = cmd.AcceptOfflineAppointment;
            tB.AcceptOnlineAppointment = cmd.AcceptOnlineAppointment;
            tB.Title = cmd.Title;
            tB.OfficeEmail = cmd.OfficeEmail;
        }

        
    }
}