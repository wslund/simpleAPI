using AutoMapper;
using MyAPI.Contexts;
using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Repository.Interfaces;
using MyAPI.Services.Interfaces;

namespace MyAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private DataContext _context;
        private readonly IMapper _mapper;

        public CompanyService(
            DataContext context,
            IMapper mapper, ICompanyRepository companyRepository)
        {
            _context = context;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyModel> Get(Guid id)
        {
            var company = await _companyRepository.Get(id);
            return _mapper.Map<CompanyEntity, CompanyModel>(company);
        }

        public async Task<List<CompanyModel>> Get(CompanyQuery query)
        {
            var t = await _companyRepository.Get(query);
            

            return _mapper.Map<List<CompanyEntity>, List<CompanyModel>>(t);
        }

        

        public async Task<CompanyModel> Create(CompanyRequest request)
        {
            var company = _mapper.Map<CompanyRequest, CompanyEntity>(request);
            var returnobject = await _companyRepository.Create(company);

            return _mapper.Map<CompanyEntity, CompanyModel>(returnobject);
        }

        public async Task<CompanyModel?> Update(Guid id, CompanyRequest request)
        {
            var company = _mapper.Map<CompanyRequest, CompanyEntity>(request);
            company.Id = id;
            var returnObject = await _companyRepository.Update(id, company);

            if (returnObject == null)
                return null;
            return _mapper.Map<CompanyEntity, CompanyModel>(returnObject);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _companyRepository.Delete(id);
        }

    }
}
