using AutoMapper;
using MyAPI.Contracts.Entities;
using MyAPI.Contracts.Models;
using MyAPI.Contracts.Requests;

namespace MyAPI.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleRequest, RoleEntity>();

        CreateMap<RoleEntity, RoleModel>().ForMember(d => d.Role, opt => opt.MapFrom(s => $"{s.Role}"));
    }
}




