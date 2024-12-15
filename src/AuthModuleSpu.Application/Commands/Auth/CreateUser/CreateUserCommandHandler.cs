using System.Text.Json;
using AuthModuleSpu.Application.Commands.Auth.CreateUser.Contracts;
using AuthModuleSpu.Application.Commands.Auth.CreateUser.Contracts.Mappers;
using AuthModuleSpu.Application.Exceptions.Auth.BadValue;
using AuthModuleSpu.Application.Validators.Auth.Email;

using AuthModuleSpu.Infrastructure.Repository.Auth;
using MediatR;


namespace AuthModuleSpu.Application.Commands.Auth.CreateUser;

public class CreateUserCommandHandler
(
    IAuthRepository authRepository    
) : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        EmailValidator.ValidateEmail(request.Email, request);
            
        var created = await authRepository.CreateUserAsync(CreateUserCommandMapper.ToInternal(request));

        if (!created)
        {
            throw new BadValueException("User with such data already exists", 
                JsonSerializer.Serialize(request));
        }
        
        return new CreateUserCommandResponse();
    }
}