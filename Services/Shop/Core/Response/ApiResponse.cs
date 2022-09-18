using Core.Errors;
using System.Net;

namespace Application.Response;

/// <summary>
/// Sample Response class with Request Url, Data, Error, Status, httpStatusCode etc.
/// </summary>
[Serializable]
public class ApiResponse
{
	/// <summary>
	/// The Sample Response Constructor
	/// </summary>
	/// <param name="requestUrl">The Request Url</param>
	/// <param name="response">The Data</param>
	/// <param name="error">The Error</param>
	/// <param name="status">The Status</param>
	/// <param name="httpStatusCode">The Http Status Code</param>
	public ApiResponse(
		string requestUrl,
		object response,
		ApiErrorObject error,
		bool status = false,
		int httpStatusCode = 500)
	{
		RequestUrl = requestUrl;
		Result = response;
		Error = error;
		Status = status;
		StatusCode = httpStatusCode;
	}

	/// <summary>
	/// The Request Url
	/// </summary>
	public string RequestUrl { get; set; }

	/// <summary>
	/// The Response Data
	/// </summary>
	public object Result { get; set; }

	/// <summary>
	/// The Response Error
	/// </summary>
	public ApiErrorObject Error { get; set; }

	/// <summary>
	/// The Response Status
	/// </summary>
	public bool Status { get; set; }

	/// <summary>
	/// The Response Http Status Code
	/// </summary>
	public int StatusCode { get; set; }
}