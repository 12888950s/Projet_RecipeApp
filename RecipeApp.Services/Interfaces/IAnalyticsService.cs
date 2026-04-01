using RecipeApp.Data.Enums;

namespace RecipeApp.Services.Interfaces
{
    public interface IAnalyticsService
    {
        // Répartition par catégorie
        Task<Dictionary<Category, int>> GetRecipeCountByCategoryAsync();
        Task<Dictionary<Category, float>> GetAvgCaloriesByCategoryAsync();

        // Analyse par type de cuisine
        Task<Dictionary<CuisineType, int>> GetRecipeCountByCuisineAsync();
        Task<Dictionary<CuisineType, float>> GetAvgCaloriesByCuisineAsync();

        // Top recettes
        Task<List<(string Title, float Calories)>> GetTopCaloricRecipesAsync(int count = 5);
        Task<float> GetGlobalAverageCaloriesAsync();
    }
}