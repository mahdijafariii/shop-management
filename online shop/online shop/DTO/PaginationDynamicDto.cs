namespace online_shop.DTO;

public class PaginationDynamicDto
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int TotalPage { get; set; }
    public long TotalResourceCount { get; set; }

    public PaginationDynamicDto(int page, int limit, long totalCount)
    {
        Page = page;
        Limit = limit;
        TotalPage = (int)Math.Ceiling(totalCount / (double)limit);
        TotalResourceCount = totalCount;
    }
}