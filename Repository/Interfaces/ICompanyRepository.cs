using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;

namespace MyAPI.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyEntity> Get(Guid id);
        Task<List<CompanyEntity>> Get(CompanyQuery query);
        Task<CompanyEntity?> Create(CompanyEntity entity);
        Task<CompanyEntity?> Update(Guid id, CompanyEntity entity);
        Task<bool> Delete(Guid id);

    }
}
