namespace online_shop.Exception;

public class DeleteAddressException : ServiceException
{
    public DeleteAddressException() : base(Resources.OtpInvalidException, StatusCodes.Status400BadRequest)
    {
    }
}