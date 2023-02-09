namespace AuthLibrary.WenApi.DTO
{
    public record NewPasswordCommand(string Email, string Password, int RecoveryPin);

}
