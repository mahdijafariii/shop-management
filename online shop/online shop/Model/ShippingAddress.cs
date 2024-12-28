namespace online_shop.Model;

public class ShippingAddress
{
    public string PostalCode { get; set; }

    public Coordinates Coordinates { get; set; }

    public string Address { get; set; }

    public int CityId { get; set; }
}

public class Coordinates
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}