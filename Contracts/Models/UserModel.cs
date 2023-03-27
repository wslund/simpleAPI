namespace MyAPI.Contracts.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }


        public string Name { get; set; }
        public int Age { get; set; }
        public CompanyModel Company { get; set; }

        public List<RoleModel> Roles { get; set; }
    }
}
