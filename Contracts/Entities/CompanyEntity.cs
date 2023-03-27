namespace MyAPI.Contracts.Entities
{
    public class CompanyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
         
        public ICollection<UserEntity> Users { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }

    }
}






//todo add relation company - user one to many: one user can only have one company. company can have many users: foreign key, navigation prop.
// todo crud function for company. Company complete crud flow.