using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.DeleteUser;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.GetUserInfo;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.UpdateUser;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.CreateUser;
using Common.Domain;

namespace AuthModuleSpu.Infrastructure.Repository.Auth;

public interface IAuthRepository
{
    public Task<User> GetUserInfoAsync(GetUserInfoQueryInternal query);
    
    public Task DeleteUserAsync(DeleteUserCommandInternal command);
   
    public Task<string> UpdateUserAsync(UpdateUserCommandInternal command);

    public Task<bool> CreateUserAsync(CreateUserCommandInternal command);
}