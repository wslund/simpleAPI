using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;

namespace MyAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyModel> Get(Guid id);
        Task<List<CompanyModel>> Get(CompanyQuery query);
        Task<CompanyModel> Create(CompanyRequest request);
        Task<CompanyModel> Update(Guid id, CompanyRequest request);
        Task<bool> Delete(Guid id);
    }
}
