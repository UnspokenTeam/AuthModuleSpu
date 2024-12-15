using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.CreateUser;
using Riok.Mapperly.Abstractions;

namespace AuthModuleSpu.Application.Commands.Auth.CreateUser.Contracts.Mappers;

[Mapper]
public static partial class CreateUserCommandMapper
{
    public static partial CreateUserCommandInternal ToInternal(CreateUserCommand request);
}