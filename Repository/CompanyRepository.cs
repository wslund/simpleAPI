using MyAPI.Contexts;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Repository.Interfaces;

namespace MyAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;

        public CompanyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CompanyEntity> Get(Guid id)
        {
            var company = _dataContext.Companies.Find(id);
            return company;
        }

        public async Task<List<CompanyEntity>> Get(CompanyQuery query)
        {
            var quaryable = _dataContext.Companies.AsQueryable();

            if (query.PageNumber <= 0)
                query.PageNumber = 1;

            if(!string.IsNullOrEmpty(query.Name))
            {
                quaryable = quaryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));
            }
            if (!string.IsNullOrEmpty(query.Discription))
            {
                quaryable = quaryable.Where(x => x.Discription.ToUpper().Contains(query.Discription.ToUpper()));
            }

            if (query.PageSize == 0)
                return quaryable.ToList();

            return await (quaryable.Skip(((query.PageNumber) - 1) * query.PageSize).Take(query.PageSize)).ToListAsync();
        }

        public async Task<CompanyEntity?> Update(Guid id, CompanyEntity entity)
        {
            var existingCompany = _dataContext.Companies.FirstOrDefault(x => x.Id == id);

            if (existingCompany == null)
                return null;

            entity.Updated = DateTime.Now;
            existingCompany.Created = existingCompany.Created;

            _dataContext.Entry(existingCompany).CurrentValues.SetValues(entity);
            await _dataContext.SaveChangesAsync();
            return _dataContext.Companies.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Delete(Guid id)
        {
            var companyInDb = _dataContext.Companies.FirstOrDefault(x => x.Id == id);

            if (companyInDb == null)
                return false;

            _dataContext.Remove(companyInDb);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyEntity?> Create(CompanyEntity entity)
        {
            var dtn = DateTime.Now;

            entity.Created = dtn;
            entity.Updated = dtn;
            

            _dataContext.Companies.Add(entity);
            _dataContext.SaveChanges();

            return _dataContext.Companies.Include(x => x.Users).Include(c => c.Roles).FirstOrDefault(x => x.Id == entity.Id);
        }
    }
}
