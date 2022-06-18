namespace API.Errors
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode = 200, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public ApiResponse(T data, int statusCode = 200, string message = null)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.  Hate leads to career change.",
                _ => null
            };
        }
    }

    public class ApiResponse : ApiResponse<string>
    {
        public ApiResponse(int statusCode, string message = null) : base(statusCode, message)
        { }
    }
}