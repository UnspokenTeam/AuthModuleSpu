using MediatR;

namespace AuthModuleSpu.Application.Commands.Auth.DeleteUser.Contracts;

public class DeleteUserCommand : IRequest<DeleteUserCommandResponse>
{
    public string Email { get; set; } = string.Empty;
}