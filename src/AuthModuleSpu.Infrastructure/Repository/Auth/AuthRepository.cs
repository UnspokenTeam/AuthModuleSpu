using AuthModuleSpu.Infrastructure.Contexts;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.DeleteUser;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.GetUserInfo;
using AuthModuleSpu.Infrastructure.Repository.Auth.Contracts.CreateUser;
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
    
    public async Task<bool> CreateUserAsync(CreateUserCommandInternal command)
    {
        var exists = await dbContext.Users.AnyAsync(
            row => row.Email == command.Email || row.Username == command.Username);
        
        if (!exists) 
        {
            await dbContext.Users.AddAsync(new User {Username = command.Username, Email = command.Email, 
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)});
            await dbContext.SaveChangesAsync();
        }

        return !exists;
    }
}