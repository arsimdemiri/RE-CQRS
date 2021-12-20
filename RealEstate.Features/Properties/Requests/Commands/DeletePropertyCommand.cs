using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Requests.Commands
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public Guid PropertyId { get; set; }
    }
}
