using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.DTOs.PropertyFeatures.Validators
{
    public class CreateFeatureDTOValidator : AbstractValidator<CreatePropertyFeatureDTO>
    {
        public CreateFeatureDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty or null");
        }
    }
}
