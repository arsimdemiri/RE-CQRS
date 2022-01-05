using MediatR;
using RealEstate.Features.DTOs.Identity;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Authentication.Requests.Commands
{
    public class GenerateTokenCommand : IRequest<AuthResponse>
    {
        public User User { get; set; }
    }
}
