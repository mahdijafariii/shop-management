namespace online_shop.Exception;

public class NotFoundException : ServiceException
{
    public NotFoundException(string resourceName)
        : base($"{resourceName} not found !!",StatusCodes.Status404NotFound)
    {
    }
}