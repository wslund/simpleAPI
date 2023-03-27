using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;

namespace MyAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleModel> Get(Guid id);
        Task<List<RoleModel>> Get(RoleQuery query);
        Task<RoleModel> Create(RoleRequest request);
        Task<RoleModel?> Update(Guid id, RoleRequest request);
        Task<bool?> Delete(Guid id);
    }
}
