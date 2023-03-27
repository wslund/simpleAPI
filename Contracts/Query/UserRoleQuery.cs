namespace MyAPI.Contracts.Query
{
    public class UserRoleQuery
    {
        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
