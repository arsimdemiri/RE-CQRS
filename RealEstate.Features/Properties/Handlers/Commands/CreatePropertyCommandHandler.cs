using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.Properties.Requests.Commands;
using RealEstate.Models;
using RealEstate.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Commands
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePropertyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyDTO> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var itemToAdd = _mapper.Map<Property>(request.PropertyViewModel);
            var addeditem = await _unitOfWork.PropertyRepository.Add(itemToAdd);
            await _unitOfWork.Save();

            return _mapper.Map<PropertyDTO>(addeditem);
        }
    }
}
