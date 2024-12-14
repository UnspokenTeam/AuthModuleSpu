using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.GetUserInfo;
using Riok.Mapperly.Abstractions;

namespace AuthModuleSpu.Application.Query.Auth.GetUserInfo.Contracts.Mappers;

[Mapper]
public static partial class GetUserInfoQueryMapper
{
    public static partial GetUserInfoQueryInternal ToInternal(GetUserInfoQuery request);
}