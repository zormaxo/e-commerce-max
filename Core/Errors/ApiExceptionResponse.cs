namespace Core.Errors;

public class ApiExceptionResponse : ApiResponse<string>
{
    public ApiExceptionResponse(string apiMessage, string exMessage = null, string stackTrace = null) : base(null, false)
    {
        Error.Message = apiMessage;
        Error.ExceptionMessage = exMessage;
        Error.Details = stackTrace;
    }

    public Error Error { get; set; } = new Error();
}

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string ExceptionMessage { get; set; }
    public string Details { get; set; }
}