namespace MyApi.Exceptions
{
    public class HttpException : Exception
    {
        public int statusCode;

        public HttpException(string message, int statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }
    }
}
