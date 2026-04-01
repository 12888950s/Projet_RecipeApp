using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipeApp.Data.Enums;

namespace RecipeApp.Data.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Le nombre de personnes est obligatoire")]
        [Range(1, 100, ErrorMessage = "Le nombre de personnes doit être entre 1 et 100")]
        public int Servings { get; set; }

        // Enum Category (Breakfast, Lunch, Dinner, Dessert, Snack)
        [Required(ErrorMessage = "La catégorie est obligatoire")]
        public Category Category { get; set; }

        // Enum CuisineType (Tunisian, French, Italian...)
        [Required(ErrorMessage = "Le type de cuisine est obligatoire")]
        public CuisineType CuisineType { get; set; }

        [Required(ErrorMessage = "Le temps de préparation est obligatoire")]
        [Range(1, 1440, ErrorMessage = "Le temps doit être entre 1 et 1440 minutes")]
        public int PrepTimeMinutes { get; set; }

        // Date de création - remplie automatiquement
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation : lien vers la table pivot RecipeIngredient
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } 
            = new List<RecipeIngredient>();
    }
}