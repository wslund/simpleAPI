namespace MyAPI.Contracts.Query
{
    public class PageListQuery
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

    }
}
