using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace MyAPI.Contracts.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        
        
        [ForeignKey(nameof(CompanyId))]
        public Guid CompanyId { get; set; }
        public CompanyEntity Company { get; set; }


        public List<UserRoleEntity> UserRoles { get; set; }

    }
}
//todo relation till company ett till flera
//todo flera till flera relation user
//todo prop namn etc
// en Role kan bara ha ett company: ett company kan ha flera roller