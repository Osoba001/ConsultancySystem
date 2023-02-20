using ConsultancySystem.WebApi.Files.Manager;
using ConsultancySystem.WebApi.Models;
using Law.Application.Commands.Lawyer;
using Law.Application.Queries.Lawyer;
using Law.Application.Response;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatR.MediatRContract;

namespace LCS.WebApi.Controllers.LawyerModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : CustomControllerBase
    {
        private readonly IFileManager _fileManager;

        public LawyerController(IMediator mediator,IFileManager fileManager) : base(mediator)
        {
            _fileManager = fileManager;
        }
        [HttpPost("review-appointment")]
        public async Task<IActionResult> ReviewAppointment([FromBody] ReviewAppointment reviewAppointment)
        {
            return await ExecuteAsync<ReviewAppointmentHandler, ReviewAppointment>(reviewAppointment);
        }

        [HttpPost("add-language")]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageToLawyer language)
        {
            return await ExecuteAsync<AddLanguageToLawyerHandler, AddLanguageToLawyer>(language);
        }
        [HttpDelete("remove-language")]
        public async Task<IActionResult> RemoveLanguage(Guid lawyerId, string language)
        {
            return await ExecuteAsync<RemoveLanguageFromLawyerHandler,RemoveLanguageFromLawyer>(new RemoveLanguageFromLawyer { LawyerId = lawyerId, Language = language });
        }

        [HttpPost("Join-department")]
        public async Task<IActionResult> JoinDepartment([FromBody] AddLawyerToDepartment addLawyerToDepartment)
        {
            return await ExecuteAsync<AddLawyerToDepartmentHandler,AddLawyerToDepartment>(addLawyerToDepartment);
        }
        
        [HttpDelete("leave-department")]
        public async Task<IActionResult> LeaveDepartment(Guid lawyerId,Guid deptId)
        {
            return await ExecuteAsync<RemoveLawyerFrmDepartmentHandler, RemoveLawyerFrmDepartment>(new RemoveLawyerFrmDepartment { LawyerId = lawyerId, DeptId = deptId });
        }

        [HttpPost("add-online-slot")]
        public async Task<IActionResult> AddOnlineWorkingSlot([FromBody] AddOnlineWorkingSlot addWorkingSlot)
        {
            return await ExecuteAsync<AddOnlineWorkinSlotHandler,AddOnlineWorkingSlot>(addWorkingSlot);
        }
        [HttpPost("add-offline-slot")]
        public async Task<IActionResult> AddOfflineWorkingSlot([FromBody] AddOfflineWorkingSlot addWorkingSlot)
        {
            return await ExecuteAsync<AddOfflineWorkinSlotHandler, AddOfflineWorkingSlot>(addWorkingSlot);
        }
        [HttpDelete("remove-online-slot")]
        public async Task<IActionResult> RemoveOnlineWorkinSlot(Guid lawyerId, List<Guid> slotIds)
        {
            return await ExecuteAsync<RemoveOnlineWorkingSlotHandler, RemoveOnlineWorkingSlot>(new RemoveOnlineWorkingSlot{LawyerId = lawyerId, SlotIds = slotIds});
        }
        [HttpDelete("remove-offline-slot")]
        public async Task<IActionResult> RemoveOfflineWorkinSlot(Guid lawyerId, List<Guid> slotIds)
        {
            return await ExecuteAsync<RemoveOfflineWorkingSlotHandler, RemoveOfflineWorkingSlot>(new RemoveOfflineWorkingSlot{ LawyerId = lawyerId, SlotIds = slotIds });
        }

        [HttpPost("verify-lawyer")]
        public async Task<IActionResult> VerifyLawyer([FromBody] VerifyLawyer verifyLawyer)
        {
            return await ExecuteAsync<VerifyLawyerHandler,VerifyLawyer>(verifyLawyer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLawyer([FromBody] UpdateLawyer lawyer)
        {
            return await ExecuteAsync<UpdateLawyerHandler,UpdateLawyer>(lawyer);
        }

        [HttpGet("by-Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await QueryNullableAsync<LawyerByIdQueryHandler, LawyerByIdQuery>(new LawyerByIdQuery { Id=id});
        }

        [HttpGet("appointment")]
        public async Task<IActionResult> GetAppointmentByLawyer(Guid lawyerId)
        {
            return await QueryAsync<AppointmentByLawyerHandler,AppointmentByLawyerQuery>(new AppointmentByLawyerQuery { LawyerId=lawyerId});
        }

        [HttpGet("by-department")]
        public async Task<IActionResult> LawyerByDepartment(Guid departmentId)
        {
            return await QueryAsync<LawyerByDepartmentHandler,LawyerByDepartmentQuery>(new LawyerByDepartmentQuery { DepartmentId = departmentId });
        }

        [HttpPatch("office-address")]
        public async Task<IActionResult> UpdateOfficeAddress([FromBody] UpdateOffice office)
        {
            return await ExecuteAsync<UpdateOfficeHandler, UpdateOffice>(office);
        }

        [HttpPost("upload-qualification")]
        public async Task<IActionResult> UploadQualification([FromBody] UploadQualificationDTO qualificationDTO)
        {
            var res = await _fileManager.SaveFile(qualificationDTO.File, qualificationDTO.UserId);
            if (res.IsSuccess)
                return Ok("Success");
            else
                return BadRequest(res.ToString());
        }

        [HttpGet("download-Certificate")]
        public FileContentResult? DownloadFile(Guid id)
        {
            string filePath = $"Files\\Qualification\\{id}.zip";
            if (Directory.Exists(filePath))
                return File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", $"{id}.zip");
            return null;

        }

    }
}
