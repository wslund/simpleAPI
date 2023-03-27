namespace MyAPI.Contracts.Requests
{
    public class RoleRequest
    {
        public string Role { get; set; }
        public Guid companyId { get; set; }
        public List<Guid> UserId { get; set; }
    }
}
