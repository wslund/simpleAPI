using MyAPI.Contracts.Query;

namespace MyAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> Get(Guid id);
        Task<List<UserEntity>> Get(UserQuery query);
        Task<UserEntity?> Update(Guid id, UserEntity query);
        Task<UserEntity?> Create(UserEntity entity);
        Task<bool> Delete(Guid id);
    }


}