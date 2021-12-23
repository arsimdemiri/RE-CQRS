using MediatR;
using RealEstate.Features.DTOs.PropertyFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.PropertyFeatures.Requests.Queries
{
    public class GetFeaturesRequest : IRequest<List<PropertyFeatureListDTO>>
    {
    }
}
