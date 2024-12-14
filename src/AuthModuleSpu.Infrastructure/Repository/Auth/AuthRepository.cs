using AuthModuleSpu.Infrastructure.Contexts;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.DeleteUser;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.GetUserInfo;
using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthModuleSpu.Infrastructure.Repository.Auth;

public class AuthRepository
(
    ApplicationDbContext dbContext
) : IAuthRepository
{
    public async Task<User> GetUserInfoAsync(GetUserInfoQueryInternal query)
    {
        return await dbContext.Users.FirstAsync(row => row.Email == query.Email);
    }

    public async Task DeleteUserAsync(DeleteUserCommandInternal command)
    {
        var user = await dbContext.Users.FirstAsync(row => row.Email == command.Email);
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }
}