namespace MyAPI.Contracts.Query
{
    public class UserQuery: PageListQuery
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
