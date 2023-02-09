namespace AuthLibrary.WenApi.DTO
{
    public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword);

}
