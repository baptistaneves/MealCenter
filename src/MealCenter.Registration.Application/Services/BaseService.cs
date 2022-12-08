using FluentValidation;
using MealCenter.Core.DomainObjects;

namespace MealCenter.Registration.Application.Services
{
    public abstract class BaseService
    {
        protected bool Validate<TValidator, TRequest>(TValidator validation, TRequest entity) 
            where TValidator : AbstractValidator<TRequest> where TRequest : class
        {
            var validationResult = validation.Validate(entity);

            if (!validationResult.IsValid)
            {
                /////
                
                return false;
            }

            return true;
        }
    }
}
