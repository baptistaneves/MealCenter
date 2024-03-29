﻿using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Restaurants
{
    public class CreateTable
    {
        [Required(ErrorMessage = "Restaurant is required")]
        public Guid RestaurantId { get; set; }

        [Required(ErrorMessage = "Table number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Table number must greater than 0")]
        public int TableNumber { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateTable
    {
        [Required(ErrorMessage = "Table number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Table number must greater than 0")]
        public int TableNumber { get; set; }
    }
}
