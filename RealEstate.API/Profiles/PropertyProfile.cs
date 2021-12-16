using AutoMapper;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Models;

namespace RealEstate.API.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, CreatePropertyDTO>().ReverseMap();
        }
    }
}
