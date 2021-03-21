namespace Exceptionhandling.ExceptionHandling._Errors
{
    public class ApiExceptions
    {
        public ApiExceptions(int statusCode, string message = null, string stackTrace = null)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.StackTrace = stackTrace;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}