namespace MealCenter.API.Contracts.Common
{
    public class ApiException
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public ApiException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
