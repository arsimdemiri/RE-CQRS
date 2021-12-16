﻿using AutoMapper;
using MediatR;
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
    public class GetPropertiesListRequestHandler : IRequestHandler<GetPropertiesListRequest, PropertyPaginatedList>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPropertiesListRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PropertyPaginatedList> Handle(GetPropertiesListRequest request, CancellationToken cancellationToken)
        {
            var pagedList = await this._unitOfWork.PropertyRepository.PaginatedProperties(request.page, request.pageSize);

            var items = _mapper.Map<List<CreatePropertyDTO>>(pagedList.Items);

            return new PropertyPaginatedList
            {
                Items = items,
                CurrentPage = pagedList.CurrentPage,
                TotalItems = pagedList.TotalItems,
                TotalPages = pagedList.TotalPages
            };

        }
    }
}
