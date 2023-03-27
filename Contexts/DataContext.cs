using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyAPI.Contracts.Entities;


namespace MyAPI.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)    
    {

    }

   
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    public DbSet<UserRoleEntity> UserRoles { get; set; }

    //public DbSet<UserRoleEntity> UserRoles { get; set; }

}
