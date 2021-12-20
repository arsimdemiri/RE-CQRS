using MediatR;
using RealEstate.Features.DTOs.Common;
using RealEstate.Features.DTOs.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Requests.Queries
{
    public class GetPropertiesListRequest : IRequest<PaginatedList<PropertyListDTO>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
