using MyAPI.Contracts.Query;

namespace MyAPI.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleEntity?> Get(Guid id);
        Task<List<RoleEntity>> Get(RoleQuery query);
        Task<RoleEntity?> Create(RoleEntity entity);
        Task<RoleEntity?> Update(Guid id, RoleEntity query);
        Task<bool> Delete(Guid id);
       
    }
}
