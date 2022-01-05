using RealEstate.Features.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealEstate.Features.Authentication.Requests.Commands;
using RealEstate.Features.DTOs.Identity;
using RealEstate.Features.DTOs.Identity.Validators;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Authentication.Handlers.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegistrationResponse>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegistrationRequestValidator();
            var validationResult = await validator.ValidateAsync(request.Model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var existingUser = await _userManager.FindByNameAsync(request.Model.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.Model.UserName}' already exists.");
            }

            var user = new User
            {
                Email = request.Model.Email,
                FirstName = request.Model.FirstName,
                LastName = request.Model.LastName,
                UserName = request.Model.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Model.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Model.Password);

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
                throw new Exception($"Email {request.Model.Email } already exists.");
            }
        }
    }
}
