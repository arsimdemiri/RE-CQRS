using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Features.Authentication.Requests.Commands;
using RealEstate.Features.Authentication.Requests.Queries;
using RealEstate.Features.DTOs.Identity;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest model)
        {
            return Ok(await _mediator.Send(new LoginRequest() { LoginModel = model }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand() { Model = request }));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenRequest model) 
        {
            return Ok(await _mediator.Send(new VerifyAndGenerateTokenCommand() { TokenRequest = model}));
        }
    }
}
