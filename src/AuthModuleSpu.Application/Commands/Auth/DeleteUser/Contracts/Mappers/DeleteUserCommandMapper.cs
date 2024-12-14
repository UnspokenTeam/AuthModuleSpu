using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.DeleteUser;
using Riok.Mapperly.Abstractions;

namespace AuthModuleSpu.Application.Commands.Auth.DeleteUser.Contracts.Mappers;

[Mapper]
public static partial class DeleteUserCommandMapper
{
    public static partial DeleteUserCommandInternal ToInternal(DeleteUserCommand request);
}