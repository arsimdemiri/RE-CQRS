using MediatR;
using RealEstate.Features.DTOs.Properties;

namespace RealEstate.Features.Properties.Requests.Commands
{
    public class CreatePropertyCommand : IRequest<PropertyDTO>
    {
        public PropertyDTO PropertyViewModel { get; set; }
    }
}
