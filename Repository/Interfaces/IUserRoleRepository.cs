using MyAPI.Contracts.Query;

namespace MyAPI.Repository.Interfaces
{
    public interface IUserRoleRepository
    {
        //Task<UserRoleEntity> Create(UserRoleEntity entity);
        Task<bool?> Delete(Guid id);
        Task<List<UserRoleEntity?>> Create(List<UserRoleEntity> entity);
        Task<List<UserRoleEntity>> Get(UserRoleQuery query);
        Task<List<UserRoleEntity>> Update(List<UserRoleEntity> userRoles);
        Task<UserRoleEntity?> GetById(Guid id);
    }
}
