using AuthModuleSpu.Application.Query.Auth.GetUserInfo.Contracts;
using AuthModuleSpu.Application.Query.Auth.GetUserInfo.Contracts.Mappers;
using AuthModuleSpu.Infrastructure.Repository.Auth;
using Common.Domain;
using MediatR;

namespace AuthModuleSpu.Application.Query.Auth.GetUserInfo;

public class GetUserInfoQueryHandler
(
    IAuthRepository authRepository
) : IRequestHandler<GetUserInfoQuery, User>
{
    public async Task<User> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        return await authRepository.GetUserInfoAsync(GetUserInfoQueryMapper.ToInternal(request));
    }
}