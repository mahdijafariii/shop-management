namespace online_shop.Exception;

public class AddSellerException : ServiceException
{
    public AddSellerException() : base(Resources.AddSellerException, StatusCodes.Status400BadRequest)
    {
    }
}