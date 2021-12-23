using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.PropertyFeatures;
using RealEstate.Features.Exceptions;
using RealEstate.Features.PropertyFeatures.Requests.Queries;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.PropertyFeatures.Handlers.Queries
{
    public class GetFeatureDetailsRequestHandler : IRequestHandler<GetFeatureDetailsRequest, PropertyFeatureDetailsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeatureDetailsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<PropertyFeatureDetailsDTO> Handle(GetFeatureDetailsRequest request, CancellationToken cancellationToken)
        {
            var propFeature = await _unitOfWork.FeatureRepository.Get(request.Id);

            if (propFeature == null)
                throw new NotFoundException(nameof(Feature), request.Id);

            return _mapper.Map<PropertyFeatureDetailsDTO>(propFeature);
        }
    }
}
