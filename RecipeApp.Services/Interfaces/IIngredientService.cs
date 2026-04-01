using RecipeApp.Data.Entities;

namespace RecipeApp.Services.Interfaces
{
    public interface IIngredientService
    {
        // GET ALL — récupérer tous les ingrédients
        Task<List<Ingredient>> GetAllAsync();

        // GET BY ID — récupérer un ingrédient par son Id
        Task<Ingredient?> GetByIdAsync(int id);

        // CREATE — ajouter un nouvel ingrédient
        Task<Ingredient> CreateAsync(Ingredient ingredient);

        // UPDATE — modifier un ingrédient existant
        Task<Ingredient?> UpdateAsync(int id, Ingredient ingredient);

        // DELETE — supprimer un ingrédient
        Task<bool> DeleteAsync(int id);

        // SEARCH — rechercher par nom
        Task<List<Ingredient>> SearchAsync(string searchTerm);

        // FILTER — filtrer par unité
        Task<List<Ingredient>> GetByUnitAsync(string unit);
    }
}