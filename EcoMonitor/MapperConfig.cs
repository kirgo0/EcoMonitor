using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO.City;
using EcoMonitor.Model.DTO.Company;
using EcoMonitor.Model.DTO.EnvFactor;
using EcoMonitor.Model.DTO.News;
using EcoMonitor.Model.DTO.Passport;
using EcoMonitor.Model.DTO.Region;
using EcoMonitor.Model.DTO.RfcFactor;
using EcoMonitor.Model.DTO.UserService;

namespace EcoMonitor
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EnvFactor, EnvFactorDTO>().ReverseMap();
            CreateMap<EnvFactorCreateDTO, EnvFactor>().ReverseMap();
            CreateMap<EnvFactorUpdateDTO, EnvFactor>().ReverseMap();
            
            CreateMap<RfcFactor, RfcFactorDTO>().ReverseMap();
            CreateMap<RfcFactorCreateDTO, RfcFactor>().ReverseMap();
            CreateMap<RfcFactorUpdateDTO, RfcFactor>().ReverseMap();

            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyCreateDTO, Company>().ReverseMap();
            CreateMap<CompanyUpdateDTO, Company>().ReverseMap();

            CreateMap<Passport, PassportDTO>().ReverseMap();
            CreateMap<PassportCreateDTO, Passport>().ReverseMap();
            CreateMap<PassportUpdateDTO, Passport>().ReverseMap();

            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<RegionCreateDTO, Region>().ReverseMap();
            CreateMap<RegionUpdateDTO, Region>().ReverseMap();

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<CityCreateDTO, City>().ReverseMap();
            CreateMap<CityUpdateDTO, City>().ReverseMap();

            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<NewsCreateDTO, News>().ReverseMap();
            CreateMap<NewsUpdateDTO, News>().ReverseMap();
        }
    }
}
