using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.Identity;
using RealEstate.Services;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Login(AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            return Ok(await this._authService.Login(request));
        }

        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            return Ok(await this._authService.Register(request));
        }
    }
}
