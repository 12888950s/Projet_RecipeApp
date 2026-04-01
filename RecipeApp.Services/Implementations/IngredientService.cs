using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Data;
using RecipeApp.Data.Entities;
using RecipeApp.Services.Interfaces;

namespace RecipeApp.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        // _context = notre connexion à la base de données
        private readonly AppDbContext _context;

        // Injection de dépendance — le DbContext est fourni automatiquement
        public IngredientService(AppDbContext context)
        {
            _context = context;
        }

        // ============ GET ALL ============
        // Récupère TOUS les ingrédients de la base de données
        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients
                .OrderBy(i => i.Name)  // triés par nom A→Z
                .ToListAsync();
        }

        // ============ GET BY ID ============
        // Récupère UN ingrédient par son Id
        // Retourne null si l'ingrédient n'existe pas
        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // ============ CREATE ============
        // Ajoute un nouvel ingrédient dans la base de données
        public async Task<Ingredient> CreateAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        // ============ UPDATE ============
        // Modifie un ingrédient existant
        // Retourne null si l'ingrédient n'existe pas
        public async Task<Ingredient?> UpdateAsync(int id, Ingredient ingredient)
        {
            // On cherche d'abord l'ingrédient existant
            var existing = await _context.Ingredients.FindAsync(id);

            // Si il n'existe pas, on retourne null
            if (existing == null) return null;

            // On met à jour les propriétés
            existing.Name = ingredient.Name;
            existing.CaloriesPerUnit = ingredient.CaloriesPerUnit;
            existing.Unit = ingredient.Unit;
            existing.Description = ingredient.Description;

            await _context.SaveChangesAsync();
            return existing;
        }

        // ============ DELETE ============
        // Supprime un ingrédient
        // Retourne true si supprimé, false si introuvable
        public async Task<bool> DeleteAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null) return false;

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }

        // ============ SEARCH (LINQ) ============
        // Recherche les ingrédients dont le nom contient le texte cherché
        public async Task<List<Ingredient>> SearchAsync(string searchTerm)
        {
            return await _context.Ingredients
                .Where(i => i.Name.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        // ============ FILTER BY UNIT (LINQ) ============
        // Filtre les ingrédients par unité (ex: "100g", "pièce")
        public async Task<List<Ingredient>> GetByUnitAsync(string unit)
        {
            return await _context.Ingredients
                .Where(i => i.Unit.ToLower() == unit.ToLower())
                .OrderBy(i => i.Name)
                .ToListAsync();
        }
    }
}