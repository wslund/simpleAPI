using MyAPI.Contexts;
using MyAPI.Contracts.Query;
using MyAPI.Repository.Interfaces;

namespace MyAPI.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _dataContext;

        public RoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<RoleEntity?> Get(Guid id)
        {
            var role = await _dataContext.Roles.FirstOrDefaultAsync(x => x.Id == id); ;
            return role;
        }

        public async Task<List<RoleEntity>> Get(RoleQuery query)
        {
            var quaryable = _dataContext.Roles
                .Include(x => x.Company)
                .AsQueryable();



            if (query.PageNumber <= 0)
                query.PageNumber = 1;


            if (!string.IsNullOrEmpty(query.Role))
            {
                quaryable = quaryable.Where(x => x.Role.ToUpper().Contains(query.Role.ToUpper()));
            }

            if (query.PageSize == 0)
                return quaryable.ToList();

            return await (quaryable.Skip(((query.PageNumber) - 1) * query.PageSize).Take(query.PageSize)).ToListAsync();
        }

        public async Task<RoleEntity?> Create(RoleEntity entity)
        {
            var dtn = DateTime.Now;

            entity.Created = dtn;
            entity.Update = dtn;

            _dataContext.Roles.Add(entity);
            _dataContext.SaveChanges();

            return _dataContext.Roles.Include(x => x.Company).Include(c => c.UserRoles).FirstOrDefault(i => i.Id == entity.Id);
        }



        public async Task<RoleEntity?> Update(Guid id, RoleEntity entity)
        {
            var roleInDb = _dataContext.Roles.FirstOrDefault(x => x.Id == id);

            if (roleInDb == null)
                return null;


            entity.Update = DateTime.Now;
            entity.Created = roleInDb.Created;


            _dataContext.Entry(roleInDb).CurrentValues.SetValues(entity);

            return await _dataContext.SaveChangesAsync() > 0
                ? _dataContext.Roles.FirstOrDefault(x => x.Id == id)
                : null;

        }
        public async Task<bool> Delete(Guid id)
        {

            var roleInDb = _dataContext.Roles.FirstOrDefault(x => x.Id == id);

            if (roleInDb == null)
                return false;


            _dataContext.Remove(roleInDb);
            await _dataContext.SaveChangesAsync();
            return true;

        }

        
    }
}
