using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.DTOs.Properties.Validators;
using RealEstate.Features.Exceptions;
using RealEstate.Features.Properties.Requests.Commands;
using RealEstate.Models;
using RealEstate.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Commands
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePropertyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatePropertyDTO> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePropertyDTOValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.PropertyViewModel);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var itemToAdd = _mapper.Map<Property>(request.PropertyViewModel);

            var addeditem = await _unitOfWork.PropertyRepository.Add(itemToAdd);

            await _unitOfWork.Save();

            return _mapper.Map<CreatePropertyDTO>(addeditem);
        }
    }
}
