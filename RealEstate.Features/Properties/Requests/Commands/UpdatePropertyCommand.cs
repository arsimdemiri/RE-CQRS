using MediatR;
using RealEstate.Features.DTOs.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Requests.Commands
{
    public class UpdatePropertyCommand : IRequest<bool>
    {
        public UpdatePropertyDTO PropertyToUpdate { get; set; }
    }
}
