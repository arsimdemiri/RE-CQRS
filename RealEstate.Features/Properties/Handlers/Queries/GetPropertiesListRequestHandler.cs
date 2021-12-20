using AutoMapper;
using MediatR;
using RealEstate.Features.DTOs.Common;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.Properties.Requests.Queries;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Queries
{
    public class GetPropertiesListRequestHandler : IRequestHandler<GetPropertiesListRequest, PaginatedList<PropertyListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPropertiesListRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginatedList<PropertyListDTO>> Handle(GetPropertiesListRequest request, CancellationToken cancellationToken)
        {
            var pagedList = await this._unitOfWork.PropertyRepository.PaginatedProperties(request.page, request.pageSize);

            var items = _mapper.Map<List<PropertyListDTO>>(pagedList.Items);

            return new PaginatedList<PropertyListDTO>
            {
                Items = items,
                CurrentPage = pagedList.CurrentPage,
                TotalItems = pagedList.TotalItems,
                TotalPages = pagedList.TotalPages
            };

        }
    }
}
