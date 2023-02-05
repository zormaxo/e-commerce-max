namespace Shop.Shared.Response;

public class ApiErrorObject
{
    public ApiErrorObject(object apiMessage, Exception ex = null, int? code = null)
    {
        Message = apiMessage;
        ExceptionMessage = ex?.Message;
        Details = ex?.StackTrace;
        Code = code;
        InnerExceptionMessage = ex?.InnerException?.Message;
    }

    public object Message { get; set; }

    public string ExceptionMessage { get; set; }

    public string InnerExceptionMessage { get; set; }

    public int? Code { get; set; }

    public string Details { get; set; }
}