using FluentValidation;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.DTOs.Properties.Validators
{
    public class UpdatePropertyDTOValidator : AbstractValidator<UpdatePropertyDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyDTOValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.Id)
                .NotNull()
                .WithMessage("{PropertyName} must be present")
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} must not be empty.");

            RuleFor(x => x.PropertyTypeId)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (id, token) =>
                {
                    return await _unitOfWork.PropertyRepository.Exists(id);
                })
                .WithMessage("{PropetyName} does not exit");

            RuleFor(x => x.PropertySellType)
                .NotNull();
        }


    }
}
