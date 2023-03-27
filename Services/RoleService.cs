using AutoMapper;
using MyAPI.Contexts;
using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Repository.Interfaces;
using MyAPI.Services.Interfaces;

namespace MyAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userroleRepository;

        public RoleService(
        DataContext context,
        IMapper mapper, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _context = context;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userroleRepository = userRoleRepository;
        }
        public async Task<RoleModel> Get(Guid id)
        {
            var role = await _roleRepository.Get(id);
            return _mapper.Map<RoleEntity, RoleModel>(role);
        }

        public async Task<List<RoleModel>> Get(RoleQuery query)
        {
            var role = await _roleRepository.Get(query);
            return _mapper.Map<List<RoleEntity>, List<RoleModel>>(role);
        }

        public async Task<RoleModel> Create(RoleRequest request)
        {
            var role = _mapper.Map<RoleRequest, RoleEntity>(request);

            var returnObj = await _roleRepository.Create(role);

            return _mapper.Map<RoleEntity,RoleModel>(returnObj);
        }

        public async Task<RoleModel?> Update(Guid id, RoleRequest request)
        {
            var role = _mapper.Map<RoleRequest, RoleEntity>(request);
            role.Id = id;
            var returnObject = await _roleRepository.Update(id, role);
            if (returnObject == null)
                return null;


            return _mapper.Map<RoleEntity, RoleModel>(returnObject);
        }

        public async Task<bool?> Delete(Guid id)
        {
            return await _roleRepository.Delete(id);
        }

       
    }
    
    
}
