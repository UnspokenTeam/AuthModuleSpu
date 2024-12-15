using AuthModuleSpu.Application.Commands.Auth.CreateUser.Contracts;
using System.Net.Mail;

namespace AuthModuleSpu.Application.Validators.Auth.CreateUser;

public static class CreateUserValidator
{
    public static bool ValidateEmail(CreateUserCommand createUserCommand)
    {
        if (string.IsNullOrWhiteSpace(createUserCommand.Email))
            return false;
        
        try
        {
            new MailAddress(createUserCommand.Email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}