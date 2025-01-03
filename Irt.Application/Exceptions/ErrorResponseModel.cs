namespace Irt.Application.Exceptions
{
    public class ErrorResponseModel
    {
        public string Type { get; set;}
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponseModel(Exception exception)
        {
            Type = exception.GetType().Name;
            Message = exception.Message;
            StackTrace = exception.ToString();
        }
    }
}