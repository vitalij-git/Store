
namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultErrorMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }


        private string GetDefaultErrorMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Invalid request. Please check your input and try again.",
                401 => "Access denied. Please log in to continue.",
                404 => "Resource not found. The requested page does not exist.",
                500 => "Server error. Please try again later.",
                _ => null
            };
        }
    }
}
