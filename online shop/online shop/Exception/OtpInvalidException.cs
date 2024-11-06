namespace online_shop.Exception;

public class OtpInvalidException : ServiceException
{
    public OtpInvalidException(string message, int statusCode) : base(Resources.OtpInvalidException, StatusCodes.Status400BadRequest)
    {
    }
}