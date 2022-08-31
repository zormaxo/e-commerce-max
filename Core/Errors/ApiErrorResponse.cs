namespace Core.Errors;

public class ApiErrorResponse<T> : ApiResponse<string>
{
    public ApiErrorResponse(T apiMessage, string exMessage = null, string stackTrace = null) : base(false)
    {
        Error.Message = apiMessage;
        Error.ExceptionMessage = exMessage;
        Error.Details = stackTrace;
    }

    public Error<T> Error { get; set; } = new Error<T>();
}

public class ApiErrorResponse : ApiErrorResponse<string>
{
    public ApiErrorResponse(string apiMessage, string exMessage = null, string stackTrace = null)
        : base(apiMessage, exMessage, stackTrace) { }
}

public class Error<T>
{
    public int Code { get; set; }
    public T Message { get; set; }
    public string ExceptionMessage { get; set; }
    public string Details { get; set; }
}