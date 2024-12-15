using AuthModuleSpu.Application.Commands.Auth.DeleteUser.Contracts;
using AuthModuleSpu.Application.Commands.Auth.UpdateUser.Contracts;
using AuthModuleSpu.Application.Query.Auth.GetUserInfo.Contracts;
using Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthModuleSpu.Presentation.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpGet(nameof(GetUserInfo))]
    [Authorize]
    public async Task<User> GetUserInfo()
    {
        var email = User.FindFirst("email")?.Value!;

        return await mediator.Send(new GetUserInfoQuery{ Email = email });
    }
    
    [HttpDelete(nameof(DeleteUser))]
    [Authorize]
    public async Task<DeleteUserCommandResponse> DeleteUser()
    {
        var email = User.FindFirst("email")?.Value!;

        return await mediator.Send(new DeleteUserCommand{ Email = email });
    }
    
    [HttpPut(nameof(UpdateUser))]
    [Authorize]
    public async Task<UpdateUserCommandResponse> UpdateUser([FromBody] UpdateUserCommandBody updateUserCommandBody)
    {
        var email = User.FindFirst("email")?.Value!;
        
        return await mediator.Send(new UpdateUserCommand {OldEmail = email, Username = updateUserCommandBody.Username,
            Email = updateUserCommandBody.Email});
    }
}