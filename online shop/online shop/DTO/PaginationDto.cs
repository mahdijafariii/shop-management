namespace online_shop.DTO;

public class PaginationDto
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int TotalPage { get; set; }
    public int TotalResourceCount { get; set; }

    public PaginationDto(int page, int limit, int totalCount)
    {
        Page = page;
        Limit = limit;
        TotalPage = (int)Math.Ceiling(totalCount / (double)limit);
        TotalResourceCount = totalCount;
    }
}