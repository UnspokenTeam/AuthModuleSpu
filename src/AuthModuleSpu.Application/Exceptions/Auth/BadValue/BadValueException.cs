namespace AuthModuleSpu.Application.Exceptions.Auth.BadValue;

public class BadValueException(string message, string data): BaseCustomException(400, message, data)
{

}