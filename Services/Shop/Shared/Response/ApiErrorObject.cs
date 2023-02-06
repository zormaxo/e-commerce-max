namespace Shop.Shared.Response;

public class ApiErrorObject
{
    public ApiErrorObject(object apiMessage, Exception? ex = null, int? code = null)
    {
        Message = apiMessage;
        Code = code;

        if (ex is not null)
        {
            ExceptionMessage = ex.Message;
            InnerExceptionMessage = ex.InnerException?.Message ?? string.Empty;
            Details = ex.StackTrace ?? string.Empty;
        }
    }

    public object Message { get; set; }

    public string ExceptionMessage { get; set; } = string.Empty;

    public string InnerExceptionMessage { get; set; } = string.Empty;

    public int? Code { get; set; }

    public string Details { get; set; } = string.Empty;
}