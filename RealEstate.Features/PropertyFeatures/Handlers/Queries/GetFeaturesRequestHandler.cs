using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.PropertyFeatures;
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
    public class GetFeaturesRequestHandler : IRequestHandler<GetFeaturesRequest, List<PropertyFeatureListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeaturesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<PropertyFeatureListDTO>> Handle(GetFeaturesRequest request, CancellationToken cancellationToken)
        {
            var propFeatures = await _unitOfWork.FeatureRepository.GetAll();

            return _mapper.Map<List<Feature>, List<PropertyFeatureListDTO>>(propFeatures);
        }
    }
}
