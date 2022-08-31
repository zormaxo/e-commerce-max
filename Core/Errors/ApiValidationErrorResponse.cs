using Core.Errors;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base("Api validation error occured...")
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}