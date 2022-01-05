using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Features.Authentication.Requests.Commands;
using RealEstate.Features.Authentication.Requests.Queries;
using RealEstate.Models;
using RealEstate.Features.DTOs.Identity;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Features.Exceptions;
using RealEstate.Features.DTOs.Identity.Validators;

namespace RealEstate.Features.Authentication.Handlers.Queries
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public LoginRequestHandler(UserManager<User> userManager,IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var validator = new AuthRequestValidator();
            var validationResult = await validator.ValidateAsync(request.LoginModel);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var user = await _userManager.FindByNameAsync(request.LoginModel.Username);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.LoginModel.Username);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.LoginModel.Password);

            if (!result)
            {
                throw new BadRequestException($"Credentials for '{request.LoginModel.Username} aren't valid'.");
            }

            var response = await _mediator.Send(new GenerateTokenCommand() { User = user });

            return response;
        }

        
    }
}
