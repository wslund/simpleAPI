﻿namespace MyAPI.Contracts.Requests
{
    public class UserRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
