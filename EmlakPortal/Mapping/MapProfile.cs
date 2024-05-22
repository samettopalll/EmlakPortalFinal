using AutoMapper;
using EmlakPortal.Dtos;
using EmlakPortal.Models;

namespace EmlakPortal.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Land, LandDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<Property, PropertyDtoForGet>().ReverseMap();
            CreateMap<Land, LandDtoForGet>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
