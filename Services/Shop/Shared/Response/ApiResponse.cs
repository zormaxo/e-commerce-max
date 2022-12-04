using Shop.Core.Response;

namespace Shop.Shared.Response;

/// <summary>
/// Sample Response class with Request Url, Data, Error, Status, httpStatusCode etc.
/// </summary>
[Serializable]
public class ApiResponse
{
    public ApiResponse(string requestUrl, object response, ApiErrorObject error, bool status = false, int httpStatusCode = 500)
    {
        RequestUrl = requestUrl;
        Result = response;
        Error = error;
        Status = status;
        StatusCode = httpStatusCode;
    }

    public bool Status { get; set; }

    public object Result { get; set; }

    public ApiErrorObject Error { get; set; }

    public string RequestUrl { get; set; }

    public int StatusCode { get; set; }
}