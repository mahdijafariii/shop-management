namespace online_shop.Exception;

public class UpdateSellerException : ServiceException
{
    public UpdateSellerException() : base(Resources.UpdateSellerException, StatusCodes.Status400BadRequest)
    {
    }
}