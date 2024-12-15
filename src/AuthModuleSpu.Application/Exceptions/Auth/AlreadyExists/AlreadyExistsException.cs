namespace AuthModuleSpu.Application.Exceptions.Auth.AlreadyExists;

public class AlreadyExistsException(string message, string data): BaseCustomException(400, message, data)
{
    
}