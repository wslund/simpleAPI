using AutoMapper;
using MyAPI.Contracts.Models;
using MyAPI.Contracts.Requests;

namespace MyAPI.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyRequest, CompanyEntity>();
            CreateMap<CompanyEntity, CompanyModel>().ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Discription,
                 opt => opt.MapFrom(s => s.Discription));
        }
    }
}
