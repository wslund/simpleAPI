using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;

namespace MyAPI.services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Get(Guid id);
        Task<List<UserModel>> Get(UserQuery query);
        Task<UserModel> Create(UserRequest request);
        Task<UserModel?> Update(Guid id, UserRequest request);
        Task<bool?> Delete(Guid id);

    }
}
