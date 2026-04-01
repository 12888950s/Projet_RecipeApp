using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipeApp.Data.Entities;

namespace RecipeApp.Data.Entities
{
    public class Ingredient
    {
        // Clé primaire - générée automatiquement par la BDD
        public int Id { get; set; }

        // [Required] = champ obligatoire (Data Annotation)
        // [StringLength] = longueur maximale
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
        public string Name { get; set; } = string.Empty;

        // [Range] = valeur minimale et maximale
        [Required(ErrorMessage = "Les calories sont obligatoires")]
        [Range(0, 10000, ErrorMessage = "Les calories doivent être entre 0 et 10000")]
        public float CaloriesPerUnit { get; set; }

        [Required(ErrorMessage = "L'unité est obligatoire")]
        [StringLength(20, ErrorMessage = "L'unité ne peut pas dépasser 20 caractères")]
        public string Unit { get; set; } = string.Empty;  // ex: "g", "ml", "pièce"

        // ? = optionnel (peut être null)
        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation : lien vers la table pivot RecipeIngredient
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } 
            = new List<RecipeIngredient>();
    }
}