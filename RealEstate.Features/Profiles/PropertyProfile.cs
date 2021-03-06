using AutoMapper;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Models;

namespace RealEstate.Features.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, CreatePropertyDTO>().ReverseMap();
            CreateMap<Property, PropertyListDTO>();
            CreateMap<Property, PropertyDetailsDTO>()
                .ForMember(x => x.PropertyType, o => o.MapFrom(x => x.PropertyType.Name));
        }
    }
}
