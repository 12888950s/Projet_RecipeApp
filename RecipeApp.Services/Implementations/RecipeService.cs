using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Data;
using RecipeApp.Data.Entities;
using RecipeApp.Data.Enums;
using RecipeApp.Services.Interfaces;

namespace RecipeApp.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.Now;
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> UpdateAsync(int id, Recipe updated)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return null;

            recipe.Title = updated.Title;
            recipe.Description = updated.Description;
            recipe.Servings = updated.Servings;
            recipe.Category = updated.Category;
            recipe.CuisineType = updated.CuisineType;
            recipe.PrepTimeMinutes = updated.PrepTimeMinutes;

            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Recipe?> AddIngredientAsync(int recipeId, int ingredientId, float quantity, string? notes)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (recipe == null || ingredient == null) return null;

            var ri = new RecipeIngredient
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                Quantity = quantity,
                Notes = notes
            };
            _context.RecipeIngredients.Add(ri);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(recipeId);
        }

        public async Task<bool> RemoveIngredientAsync(int recipeId, int ingredientId)
        {
            var ri = await _context.RecipeIngredients
                .FindAsync(recipeId, ingredientId);
            if (ri == null) return false;

            _context.RecipeIngredients.Remove(ri);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateIngredientQuantityAsync(int recipeId, int ingredientId, float newQuantity)
        {
            var ri = await _context.RecipeIngredients
                .FindAsync(recipeId, ingredientId);
            if (ri == null) return false;

            ri.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<float> GetTotalCaloriesAsync(int recipeId)
        {
            var recipe = await GetByIdAsync(recipeId);
            if (recipe == null) return 0;

            return recipe.RecipeIngredients
                .Sum(ri => ri.Quantity * ri.Ingredient.CaloriesPerUnit);
        }

        public async Task<float> GetCaloriesPerPersonAsync(int recipeId)
        {
            var recipe = await GetByIdAsync(recipeId);
            if (recipe == null) return 0;

            var total = await GetTotalCaloriesAsync(recipeId);
            return total / recipe.Servings;
        }

        public async Task<List<Recipe>> GetByCategoryAsync(Category category)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Where(r => r.Category == category)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetByCuisineTypeAsync(CuisineType cuisineType)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Where(r => r.CuisineType == cuisineType)
                .ToListAsync();
        }

        public async Task<List<Recipe>> SearchAsync(string searchTerm)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Where(r => r.Title.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }
    }
}