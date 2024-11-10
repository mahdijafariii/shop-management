namespace online_shop.Exception;

public class OperationFailedException : ServiceException
{
    public OperationFailedException() : base(Resources.OperationFiledException, StatusCodes.Status400BadRequest)
    {
    }
}