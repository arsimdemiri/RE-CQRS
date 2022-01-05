using MediatR;
using RealEstate.Features.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Authentication.Requests.Commands
{
    public class RegisterUserCommand : IRequest<RegistrationResponse>
    {
        public RegistrationRequest Model { get; set; }
    }
}
