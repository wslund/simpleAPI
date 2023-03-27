using MyAPI.Contexts;
using MyAPI.Contracts.Query;
using MyAPI.Repository.Interfaces;

namespace MyAPI.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DataContext _dataContext;
        private IQueryable<UserEntity> _users;


        public UserRoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _users = _dataContext.Users
                 .Include(x => x.Company)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role);
        }

        public async Task<List<UserRoleEntity?>> Create(List<UserRoleEntity> entity)
        {
            _dataContext.UserRoles.AddRange(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<UserRoleEntity>> Get(UserRoleQuery query)
        {
            var quaryable = _dataContext.UserRoles.AsQueryable();

            if (query.UserId != null)
            {
                quaryable = quaryable.Where(c => c.UserId == query.UserId);
            }
            if(query.RoleId != null)
            {
                quaryable.Where(c => c.RoleId == query.RoleId);
            }

            return await (quaryable.ToListAsync()); 
        }

        public async Task<List<UserRoleEntity>> Update(List<UserRoleEntity> userRoles)
        {
            _dataContext.UserRoles.AddRange(userRoles);
            await _dataContext.SaveChangesAsync();

            return userRoles;
        }

        private async Task<List<UserRoleEntity>> Query(IQueryable<UserRoleEntity> quaryable, UserRoleQuery query)
        {
            if (query.UserId != null)
            {
                quaryable = quaryable.Where(x => x.UserId == query.UserId);
            }

            if (query.RoleId != null)
            {
                quaryable = quaryable.Where(x => x.RoleId == query.RoleId);
            }

            return await quaryable.ToListAsync();
        }

        public async Task<bool> Delete(List<UserRoleEntity> ids)
        {
            _dataContext.RemoveRange(ids);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserRoleEntity?> GetById(Guid id)
        {
            var userRole = _dataContext.UserRoles.FirstOrDefault(x => x.Id == id);
            return userRole;
        }

        public async Task<bool?> Delete(Guid id)
        {
            var userRole = _dataContext.UserRoles.FirstOrDefault(x => x.Id == id);
            
            if (userRole == null)
                return false;
                
            _dataContext.Remove(userRole);
            _dataContext.SaveChanges();
            return true;
        }
    }


}
