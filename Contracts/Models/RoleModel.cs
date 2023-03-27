namespace MyAPI.Contracts.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
       
        public string Role { get; set; }
        public CompanyModel Company { get; set; }
    }
}
