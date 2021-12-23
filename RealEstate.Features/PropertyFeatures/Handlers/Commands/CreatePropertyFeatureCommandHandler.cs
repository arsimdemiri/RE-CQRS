using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.PropertyFeatures;
using RealEstate.Features.DTOs.PropertyFeatures.Validators;
using RealEstate.Features.Exceptions;
using RealEstate.Features.PropertyFeatures.Requests.Commands;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.PropertyFeatures.Handlers.Commands
{
    public class CreatePropertyFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, CreatePropertyFeatureDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePropertyFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatePropertyFeatureDTO> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateFeatureDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreatePropertyFeatureDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var itemToAdd = _mapper.Map<Feature>(request.CreatePropertyFeatureDTO);

            var addedItem = await _unitOfWork.FeatureRepository.Add(itemToAdd);
            await _unitOfWork.Save();

            return _mapper.Map<CreatePropertyFeatureDTO>(addedItem);
        }
    }
}
