namespace AuthLibrary.WenApi.DTO
{
    public record ConfirmPinCommand(string Email, int RecoveryPin);

}
