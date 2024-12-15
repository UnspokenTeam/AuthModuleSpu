namespace AuthModuleSpu.Application.Exceptions;

public class BaseCustomException : Exception
{
    public int StatusCode { get; set; }    
    public string RequestData { get; set; }

    public BaseCustomException(int statusCode, string message, string requestData)
        : base(message)
    {
        StatusCode = statusCode;
        RequestData = requestData;
    }
}