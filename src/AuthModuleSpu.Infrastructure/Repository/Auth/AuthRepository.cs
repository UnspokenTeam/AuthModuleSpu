using AuthModuleSpu.Infrastructure.Contexts;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.DeleteUser;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.GetUserInfo;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.UpdateUser;
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
    
    public async Task<string> UpdateUserAsync(UpdateUserCommandInternal command)
    {
        var entity = await dbContext.Users.FirstOrDefaultAsync(row => row.Email == command.OldEmail);
        if (entity == null)
        {
            return $"User with email {command.OldEmail} not exists";
        }

        var duplicate = await dbContext.Users.AnyAsync(
            row => row.Email == command.Email || row.Username == command.Username);
        
        if (!duplicate)
        {
            entity.Username = command.Username;
            entity.Email = command.Email;
            await dbContext.SaveChangesAsync();
            return string.Empty;
        }
        return "Username or email already exists";
    }
}