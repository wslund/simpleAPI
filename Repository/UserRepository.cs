using MyAPI.Contexts;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Repository.Interfaces;

namespace MyAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _dataContext;
        private IQueryable<UserEntity> _users;
        

        public UserRepository(DataContext dataContext)
        {
            
            _dataContext = dataContext;
            _users = _dataContext.Users
                 .Include(x => x.Company)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }


        public async Task<List<UserEntity>> Get(UserQuery query)
        {
            var result = Query(_users, query);

            return await result;
        }

        public async Task<UserEntity?> Create(UserEntity entity)
        {
            var dtn = DateTime.Now;

            entity.Created = dtn;
            entity.Update = dtn;

            _dataContext.Users.Add(entity);
            _dataContext.SaveChanges();

            return _dataContext.Users.Include(x => x.Company).Include(r => r.UserRoles).FirstOrDefault(x => x.Id == entity.Id);
        }

        public async Task<UserEntity?> Update(Guid id, UserEntity entity)
        {
            var userInDb = _dataContext.Users.FirstOrDefault(x => x.Id == id);

            if (userInDb == null)
                return null;

            entity.Update = DateTime.Now;
            entity.Created = userInDb.Created;


            _dataContext.Entry(userInDb).CurrentValues.SetValues(entity);
            await _dataContext.SaveChangesAsync();
            return _dataContext.Users.Include(x => x.Company).Include(r => r.UserRoles).FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Delete(Guid id)
        {
            var userInDb = _dataContext.Users.FirstOrDefault(x => x.Id == id);

            if (userInDb == null)
                return false;


            _dataContext.Remove(userInDb);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        private async Task<List<UserEntity>> Query(IQueryable<UserEntity>quaryable, UserQuery query)
        {
            if (query.PageNumber <= 0)
                query.PageNumber = 1;


            if (!string.IsNullOrEmpty(query.FirstName))
            {
                quaryable = quaryable.Where(x => x.FirstName.ToUpper().Contains(query.FirstName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(query.LastName))
            {
                quaryable = quaryable.Where(x => x.LastName.ToUpper().Contains(query.LastName.ToUpper()));
            }

            if (query.PageSize == 0)
                return quaryable.ToList();

            return await (quaryable.Skip(((query.PageNumber) - 1) * query.PageSize).Take(query.PageSize)).ToListAsync();
        }

        
    }

}
