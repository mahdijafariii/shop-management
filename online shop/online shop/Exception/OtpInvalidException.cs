namespace online_shop.Exception;

public class OtpInvalidException : ServiceException
{
    public OtpInvalidException() : base(Resources.OtpInvalidException, StatusCodes.Status400BadRequest)
    {
    }
}