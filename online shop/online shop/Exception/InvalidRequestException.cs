namespace online_shop.Exception;

public class InvalidRequestException : ServiceException
{
    public InvalidRequestException(string message, int statusCode)
        : base(message,statusCode)
    {
    }
}