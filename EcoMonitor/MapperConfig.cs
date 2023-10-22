using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO;

namespace EcoMonitor
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EnvFactor, EnvFactorDTO>().ReverseMap();
            CreateMap<EnvFactorCreateDTO, EnvFactor>().ReverseMap();
            CreateMap<EnvFactorUpdateDTO, EnvFactor>().ReverseMap();

            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyCreateDTO, Company>().ReverseMap();
            CreateMap<CompanyUpdateDTO, Company>().ReverseMap();

            CreateMap<Passport, PassportDTO>().ReverseMap();
            CreateMap<PassportCreateDTO, Passport>().ReverseMap();
            CreateMap<PassportUpdateDTO, Passport>().ReverseMap();
        }
    }
}
