namespace online_shop.DTO;

public class UpdateSellerRequestDto
{
    public string RequestId { get; set; }
    public string Status { get; set; }
    public string? AdminComment { get; set; }

    public UpdateSellerRequestDto(string requestId, string status, string? adminComment = null)
    {
        RequestId = requestId;
        Status = status;
        AdminComment = adminComment;
    }
}