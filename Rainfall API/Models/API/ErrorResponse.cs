namespace Rainfall_API.Models.API
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        
        public List<ErrorDetail>? Detail { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public ErrorResponse(string message, List<ErrorDetail>? detail) : this(message)
        {
            Detail = detail;
        }
    }
}