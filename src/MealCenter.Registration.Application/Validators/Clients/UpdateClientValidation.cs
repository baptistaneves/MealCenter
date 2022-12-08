using FluentValidation;
using MealCenter.Registration.Application.Contracts.Clients;

namespace MealCenter.Registration.Application.Validators.Clients
{
    internal class UpdateClientValidation : AbstractValidator<UpdateClient>
    {
        public UpdateClientValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters");
        }
    }
}
