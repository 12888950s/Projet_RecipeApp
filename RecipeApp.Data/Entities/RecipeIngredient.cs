using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Data.Entities
{
    // Table pivot = relie Recipe et Ingredient
    // Une recette peut avoir plusieurs ingrédients
    // Un ingrédient peut être dans plusieurs recettes
    public class RecipeIngredient
    {
        // Clé étrangère vers Recipe
        public int RecipeId { get; set; }

        // Clé étrangère vers Ingredient
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "La quantité est obligatoire")]
        [Range(0.01, 10000, ErrorMessage = "La quantité doit être positive")]
        public float Quantity { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }  // ex: "finement haché", "à température ambiante"

        // Navigation : accès à l'objet Recipe complet
        public Recipe Recipe { get; set; } = null!;

        // Navigation : accès à l'objet Ingredient complet
        public Ingredient Ingredient { get; set; } = null!;
    }
}