namespace AuthModuleSpu.Application.Exceptions.Auth.BadEmail;

public class BadEmailException(string message, string data): BaseCustomException(400, message, data)
{
    
}