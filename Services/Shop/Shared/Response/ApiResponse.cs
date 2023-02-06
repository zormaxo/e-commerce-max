namespace Shop.Shared.Response;

[Serializable]
public class ApiResponse
{
    public ApiResponse(string requestUrl, object? result, ApiErrorObject? error, bool status = false, int httpStatusCode = 500)
    {
        RequestUrl = requestUrl;
        Result = result;
        Error = error;
        Status = status;
        StatusCode = httpStatusCode;
    }

    public bool Status { get; set; }

    public object? Result { get; set; }

    public ApiErrorObject? Error { get; set; }

    public string RequestUrl { get; set; }

    public int StatusCode { get; set; }
}