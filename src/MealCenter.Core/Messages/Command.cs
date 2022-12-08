using FluentValidation.Results;
using MediatR;

namespace MealCenter.Core.Messages
{
    public class Command : Message, IRequest<bool>
    {
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
