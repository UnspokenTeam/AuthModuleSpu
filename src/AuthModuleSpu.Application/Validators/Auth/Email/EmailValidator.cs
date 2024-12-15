using AuthModuleSpu.Application.Exceptions.Auth.BadValue;
using System.Net.Mail;
using System.Text.Json;

namespace AuthModuleSpu.Application.Validators.Auth.Email;

public static class EmailValidator
{
    public static bool ValidateEmail<T>(string email, T request)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            throw new BadValueException($"Email '{email}' is invalid", JsonSerializer.Serialize(request));
        }
    }
}