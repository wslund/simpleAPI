using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyAPI.Contracts.Entities;

public class UserEntity
{

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Age { get; set; }
    public DateTime Created { get; set; }
    public DateTime Update { get; set; }


    
    [ForeignKey(nameof(CompanyId))]
    public Guid CompanyId { get; set; }

    public CompanyEntity Company { get; set; }


    public List<UserRoleEntity> UserRoles { get; set; }


}



