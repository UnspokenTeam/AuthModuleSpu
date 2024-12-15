using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.UpdateUser;
using Riok.Mapperly.Abstractions;

namespace AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts.Mappers;

[Mapper]
public static partial class UpdateUserCommandMapper
{
    public static partial UpdateUserCommandInternal ToInternal(UpdateUserCommand request);
}