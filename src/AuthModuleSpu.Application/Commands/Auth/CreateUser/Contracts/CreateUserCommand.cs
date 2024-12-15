using MediatR;

namespace AuthModuleSpu.Application.Commands.Auth.CreateUser.Contracts;

public class CreateUserCommand : IRequest<CreateUserCommandResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}