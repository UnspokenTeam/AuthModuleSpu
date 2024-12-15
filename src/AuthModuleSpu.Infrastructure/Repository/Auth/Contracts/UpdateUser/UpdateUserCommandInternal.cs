namespace AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.UpdateUser;

public class UpdateUserCommandInternal
{
    public string OldEmail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}