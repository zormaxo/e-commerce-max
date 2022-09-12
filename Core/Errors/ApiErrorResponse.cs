namespace Core.Errors;

public class ApiErrorResponse
{
    public ApiErrorResponse(object apiMessage, string exMessage = null, string stackTrace = null, int? code = null)
    {
        Message = apiMessage;
        ExceptionMessage = exMessage;
        Details = stackTrace;
        Code = code;
    }

    public int? Code { get; set; }
    public object Message { get; set; }
    public string ExceptionMessage { get; set; }
    public string Details { get; set; }
}