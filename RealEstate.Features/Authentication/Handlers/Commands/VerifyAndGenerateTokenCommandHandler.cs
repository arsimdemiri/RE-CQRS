using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Features.Authentication.Requests.Commands;
using RealEstate.Features.DTOs.Identity;
using RealEstate.Features.Helpers;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Authentication.Handlers.Commands
{
    public class VerifyAndGenerateTokenCommandHandler : IRequestHandler<VerifyAndGenerateTokenCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly UserManager<User> _userManager;

        public VerifyAndGenerateTokenCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, TokenValidationParameters tokenValidationParams, UserManager<User> userManager = null)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _tokenValidationParams = tokenValidationParams;
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(VerifyAndGenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(request.TokenRequest.Token, _tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = RandomHelpers.UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    throw new Exception("Token has not yet expired");
                }

                // validation 4 - validate existence of the token
                var storedToken = await _unitOfWork.RefreshTokenRepository.GetByTokenAsync(request.TokenRequest.RefreshToken);

                if (storedToken == null)
                {
                    throw new Exception("Token does not exist");
                }

                // Validation 5 - validate if used
                if (storedToken.IsUsed)
                {
                    throw new Exception("Token has been used");
                }

                // Validation 6 - validate if revoked
                if (storedToken.IsRevoked)
                {
                    throw new Exception("Token has been revoked");
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    throw new Exception("Token doesn't match");
                }

                // update current token 

                storedToken.IsUsed = true;
                await _unitOfWork.RefreshTokenRepository.Update(storedToken);
                await _unitOfWork.Save();

                // Generate a new token
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
                return await _mediator.Send(new GenerateTokenCommand() { User = dbUser });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
