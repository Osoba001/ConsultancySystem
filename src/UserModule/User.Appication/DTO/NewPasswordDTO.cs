namespace User.Application.DTO
{
    public record NewPasswordDTO(string Email, string Password, int RecoveryPin);

}
