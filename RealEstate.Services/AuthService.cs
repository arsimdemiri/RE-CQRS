using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.Identity;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static RealEstate.Models.Shared.Constants;

namespace RealEstate.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(UserManager<User> userManager,
            IOptions<JwtSettings> jwtSettings, TokenValidationParameters tokenValidationParameters, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new Exception($"User with {request.Username} not found.");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new Exception($"Credentials for '{request.Username} aren't valid'.");
            }

            var response = await GenerateToken(user);

            //AuthResponse response = new AuthResponse
            //{
            //    Id = user.Id.ToString(),
            //    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            //    Email = user.Email,
            //    UserName = user.UserName
            //};

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new RegistrationResponse() { UserId = user.Id.ToString() };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email } already exists.");
            }
        }

        private async Task<AuthResponse> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id.ToString())
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
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(30) + Guid.NewGuid()
            };
            await _unitOfWork.RefreshTokenRepository.Add(refreshToken);
            await _unitOfWork.Save();
            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = refreshToken.Token,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id.ToString()

            };
        }

        private string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

        }
    }
}
