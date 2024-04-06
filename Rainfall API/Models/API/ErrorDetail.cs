namespace Rainfall_API.Models.API
{
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        
        public string Message { get; set; }

        public ErrorDetail(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }
}