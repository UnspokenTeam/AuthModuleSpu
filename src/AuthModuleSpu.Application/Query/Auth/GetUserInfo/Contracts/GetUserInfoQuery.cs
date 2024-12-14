using Common.Domain;
using MediatR;

namespace AuthModuleSpu.Application.Query.Auth.GetUserInfo.Contracts;

public class GetUserInfoQuery : IRequest<User>
{
    public string Email { get; set; } = string.Empty;
}