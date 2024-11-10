namespace online_shop.Exception;

public class UpdateAddressException : ServiceException
{
    public UpdateAddressException() : base(Resources.UpdateAddressException, StatusCodes.Status400BadRequest)
    {
    }
}