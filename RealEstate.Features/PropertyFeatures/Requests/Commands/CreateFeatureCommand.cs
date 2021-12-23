using MediatR;
using RealEstate.Features.DTOs.PropertyFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.PropertyFeatures.Requests.Commands
{
    public class CreateFeatureCommand : IRequest<CreatePropertyFeatureDTO>
    {
        public CreatePropertyFeatureDTO CreatePropertyFeatureDTO { get; set; }
    }
}
