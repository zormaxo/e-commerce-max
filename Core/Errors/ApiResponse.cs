namespace Core.Errors;

public class ApiResponse<T>
{
    public ApiResponse(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
    }
    public ApiResponse(T data, bool isSuccess = true)
    {
        Data = data;
        IsSuccess = isSuccess;
    }

    public T Data { get; set; }
    public bool IsSuccess { get; }
}
