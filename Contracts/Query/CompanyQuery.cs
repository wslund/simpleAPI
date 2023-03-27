namespace MyAPI.Contracts.Query
{
    public class CompanyQuery
    {
        public string? Name { get; set; }
        public string? Discription { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
