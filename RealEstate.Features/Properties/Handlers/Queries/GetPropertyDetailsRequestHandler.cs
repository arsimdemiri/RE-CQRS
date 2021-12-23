using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.Exceptions;
using RealEstate.Features.Properties.Requests.Queries;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Queries
{
    public class GetPropertyDetailsRequestHandler : IRequestHandler<GetPropertyDetailsRequest, PropertyDetailsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPropertyDetailsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PropertyDetailsDTO> Handle(GetPropertyDetailsRequest request, CancellationToken cancellationToken)
        {
            var propertyDetails = await _unitOfWork.PropertyRepository.Get(request.PropertyId);

            if (propertyDetails == null)
                throw new NotFoundException(nameof(Property), request.PropertyId);

            return _mapper.Map<PropertyDetailsDTO>(propertyDetails);
        }
    }
}
