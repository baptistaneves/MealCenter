﻿using System.ComponentModel.DataAnnotations;

namespace MealCenter.Catalog.Application.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name must not be empty")]
        [MinLength(4, ErrorMessage = "Name must be at leats 4 characters")]
        public string Name { get; set; }
    }
}
