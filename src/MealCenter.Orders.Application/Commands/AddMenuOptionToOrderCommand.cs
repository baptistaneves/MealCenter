﻿using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class AddMenuOptionToOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public Guid TableId { get; private set; }
        public Guid MenuOptionId { get; private set; }
        public Guid ProductId { get; private set; }
        public string MenuOptionName { get; private set; }
        public string ProductName { get; private set; }
        public decimal MenuOptionPrice { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int ProductQuantity { get; private set; }
        public int MenuOptionQuantity { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AddMenuOptionToOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
