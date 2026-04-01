using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Data;
using RecipeApp.Data.Enums;
using RecipeApp.Services.Interfaces;

namespace RecipeApp.Services.Implementations
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _context;

        public AnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<Category, int>> GetRecipeCountByCategoryAsync()
        {
            return await _context.Recipes
                .GroupBy(r => r.Category)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<Category, float>> GetAvgCaloriesByCategoryAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .ToListAsync();

            return recipes
                .GroupBy(r => r.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(r =>
                        r.RecipeIngredients.Sum(ri =>
                            ri.Quantity * ri.Ingredient.CaloriesPerUnit / 100)
                        / r.Servings)
                );
        }

        public async Task<Dictionary<CuisineType, int>> GetRecipeCountByCuisineAsync()
        {
            return await _context.Recipes
                .GroupBy(r => r.CuisineType)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<CuisineType, float>> GetAvgCaloriesByCuisineAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .ToListAsync();

            return recipes
                .GroupBy(r => r.CuisineType)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(r =>
                        r.RecipeIngredients.Sum(ri =>
                            ri.Quantity * ri.Ingredient.CaloriesPerUnit / 100))
                );
        }

        public async Task<List<(string Title, float Calories)>> GetTopCaloricRecipesAsync(int count = 5)
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .ToListAsync();

            return recipes
                .Select(r => (
                    r.Title,
                    r.RecipeIngredients.Sum(ri => ri.Quantity * ri.Ingredient.CaloriesPerUnit / 100)
                ))
                .OrderByDescending(x => x.Item2)
                .Take(count)
                .ToList();
        }

        public async Task<float> GetGlobalAverageCaloriesAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .ToListAsync();

            if (!recipes.Any()) return 0;

            return recipes.Average(r =>
                r.RecipeIngredients.Sum(ri =>
                    ri.Quantity * ri.Ingredient.CaloriesPerUnit / 100)
                / r.Servings);
        }
    }
}