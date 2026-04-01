using RecipeApp.Data.Entities;
using RecipeApp.Data.Enums;

namespace RecipeApp.Services.Interfaces
{
    public interface IRecipeService
    {
        // CRUD de base
        Task<List<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe?> UpdateAsync(int id, Recipe recipe);
        Task<bool> DeleteAsync(int id);

        // Gestion des ingrédients dans une recette
        Task<Recipe?> AddIngredientAsync(int recipeId, int ingredientId, float quantity, string? notes);
        Task<bool> RemoveIngredientAsync(int recipeId, int ingredientId);
        Task<bool> UpdateIngredientQuantityAsync(int recipeId, int ingredientId, float newQuantity);

        // Calculs caloriques
        Task<float> GetTotalCaloriesAsync(int recipeId);
        Task<float> GetCaloriesPerPersonAsync(int recipeId);

        // Filtrage
        Task<List<Recipe>> GetByCategoryAsync(Category category);
        Task<List<Recipe>> GetByCuisineTypeAsync(CuisineType cuisineType);
        Task<List<Recipe>> SearchAsync(string searchTerm);
    }
}