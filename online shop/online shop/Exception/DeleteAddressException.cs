namespace online_shop.Exception;

public class DeleteAddressException : ServiceException
{
    public DeleteAddressException() : base(Resources.DeleteAddressException, StatusCodes.Status400BadRequest)
    {
    }
}