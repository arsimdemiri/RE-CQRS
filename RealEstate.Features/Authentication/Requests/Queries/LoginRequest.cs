using MediatR;
using RealEstate.Features.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.Authentication.Requests.Queries
{
    public class LoginRequest : IRequest<AuthResponse>
    {
        public AuthRequest LoginModel { get; set; }
    }
}
