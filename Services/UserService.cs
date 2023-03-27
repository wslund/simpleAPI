using AutoMapper;
using MyAPI.Contexts;
using MyAPI.Contracts.Models;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Repository.Interfaces;
using MyAPI.services.Interfaces;
using System.Linq;

namespace MyAPI.Services
{


    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICompanyRepository _companyRepository;
        public UserService(
            DataContext context,
            IMapper mapper, IUserRepository userRepository,
            IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, ICompanyRepository companyRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _companyRepository = companyRepository;
        }

        public async Task<UserModel> Get(Guid id)
        {
            var user = await _userRepository.Get(id);
            
            return _mapper.Map<UserEntity, UserModel>(user);
        }


        public async Task<List<UserModel>> Get(UserQuery query)
        {
            var t = await _userRepository.Get(query);

            return _mapper.Map<List<UserEntity>, List<UserModel>>(t);
        }


        public async Task<UserModel> Create(UserRequest request)
        {
            var user = await _userRepository.Create(_mapper.Map<UserRequest, UserEntity>(request));
           
            ArgumentNullException.ThrowIfNull(user);

            var userRoles = request.RoleIds.Select(x => new UserRoleEntity
            {
                UserId = user.Id,
                RoleId = x
            }).ToList();

            var result = await _userRoleRepository.Create(userRoles);

            return await Get(user.Id); 
        }

        public async Task<UserModel?> Update(Guid id, UserRequest request)
        {
            var requestRoles = request.RoleIds.ToList();
            var dbRoles = await _userRoleRepository.Get(new UserRoleQuery { UserId = id });



            var dbRemove = dbRoles.Where(i => !requestRoles.Contains(i.RoleId)).ToList();
            foreach (var x in dbRemove)
            {
                var userRoleId = x.Id;
                await _userRoleRepository.Delete(userRoleId);
            }



            var roleIds = dbRoles.Select(x => x.RoleId).ToList();
            var userRolesAdd = requestRoles.Where(i => !roleIds.Contains(i)).ToList();
            
            var userRoles = userRolesAdd.Select(x => new UserRoleEntity
            {
                UserId = id,
                RoleId = x
            }).ToList();

            var result = await _userRoleRepository.Create(userRoles);

            var user = _mapper.Map<UserRequest, UserEntity>(request);
            user.Id= id;
            
            var returnObject = await _userRepository.Update(id, user);

            if (returnObject == null)
                return null;
            
            _mapper.Map<UserEntity, UserModel>(returnObject);

            return await Get(id);
        }

        public async Task<bool?> Delete(Guid id)
        {
            var userId = await _userRepository.Get(id);
            var userRoles = await _userRoleRepository.Get(new UserRoleQuery { UserId = id });
            return await _userRepository.Delete(id);
        }

       
    }
}
