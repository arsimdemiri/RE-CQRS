using RealEstate.Models.Identity;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
