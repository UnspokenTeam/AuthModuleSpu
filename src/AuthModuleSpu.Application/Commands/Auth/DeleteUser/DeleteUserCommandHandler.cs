using AuthModuleSpu.Application.Commands.Auth.DeleteUser.Contracts;
using AuthModuleSpu.Application.Commands.Auth.DeleteUser.Contracts.Mappers;
using AuthModuleSpu.Infrastructure.Repository.Auth;
using MediatR;

namespace AuthModuleSpu.Application.Commands.Auth.DeleteUser;

public class DeleteUserCommandHandler
(
    IAuthRepository authRepository    
) : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
{
    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await authRepository.DeleteUserAsync(DeleteUserCommandMapper.ToInternal(request));
        
        return new DeleteUserCommandResponse();
    }
}