namespace User.Application.DTO
{
    public record ChangePasswordDTO(Guid UserId, string OldPassword, string NewPassword);

}
