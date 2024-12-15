using MediatR;

namespace AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts;

public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public string OldEmail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}