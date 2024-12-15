using System.Text.Json;
using AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts;
using AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts.Mappers;
using AuthModuleSpu.Application.Exceptions.Auth.BadValue;
using AuthModuleSpu.Application.Validators.Auth.Email;
using AuthModuleSpu.Infrastructure.Repository.Auth;
using MediatR;

namespace AuthModuleSpu.Application.Commands.Auth.UpdateUser;

public class UpdateUserCommandHandler(
    IAuthRepository authRepository
) : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        EmailValidator.ValidateEmail(request.Email, request);

        var updatedStatus = await authRepository.UpdateUserAsync(UpdateUserCommandMapper.ToInternal(request));

        if (updatedStatus != string.Empty)
        {
            throw new BadValueException(updatedStatus, JsonSerializer.Serialize(request));
        }

        return new UpdateUserCommandResponse();
    }
}