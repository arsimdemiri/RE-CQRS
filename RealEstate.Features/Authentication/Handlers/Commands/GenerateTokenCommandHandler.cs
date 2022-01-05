using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Features.Authentication.Requests.Commands;
using RealEstate.Features.DTOs.Identity;
using RealEstate.Features.Helpers;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RealEstate.Models.Shared.Constants;

namespace RealEstate.Features.Authentication.Handlers.Commands
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateTokenCommandHandler(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var userClaims = await _userManager.GetClaimsAsync(request.User);
            var roles = await _userManager.GetRolesAsync(request.User);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, request.User.Email),
                new Claim(CustomClaimTypes.Uid, request.User.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                //expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                expires: DateTime.UtcNow.AddSeconds(30),
                signingCredentials: signingCredentials);

            var refreshToken = new RefreshToken()
            {
                JwtId = jwtSecurityToken.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = request.User.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomHelpers.RandomString(30) + Guid.NewGuid()
            };
            await _unitOfWork.RefreshTokenRepository.Add(refreshToken);
            await _unitOfWork.Save();
            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = refreshToken.Token,
                Email = request.User.Email,
                UserName = request.User.UserName,
                Id = request.User.Id.ToString()

            };
        }

        
    }
}
