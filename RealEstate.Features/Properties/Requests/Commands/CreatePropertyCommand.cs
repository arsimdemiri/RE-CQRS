using MediatR;
using RealEstate.Features.DTOs.Properties;

namespace RealEstate.Features.Properties.Requests.Commands
{
    public class CreatePropertyCommand : IRequest<CreatePropertyDTO>
    {
        public CreatePropertyDTO PropertyViewModel { get; set; }
    }
}
