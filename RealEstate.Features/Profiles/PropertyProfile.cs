using AutoMapper;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Models;

namespace RealEstate.Features.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Property, PropertyListDTO>();
        }
    }
}
