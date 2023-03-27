namespace MyAPI.Contracts.Query
{
    public class RoleQuery
    {
        public string? Role { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
