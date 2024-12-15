namespace AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts;

public class UpdateUserCommandBody
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}