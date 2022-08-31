namespace Core.Errors;

public class ApiResponse<T>
{
    public ApiResponse(T data, bool isSuccess = true)
    {
        Data = data;
        IsSuccess = isSuccess;
    }

    public T Data { get; set; }
    public bool IsSuccess { get; }
}

public class ApiResponse : ApiResponse<string>
{
    public ApiResponse(string message) : base(null, false)
    {
        Message = message;
    }

    public string Message { get; }
}