using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Entities;
using RecipeApp.Data.Enums;

namespace RecipeApp.Data.Data
{
    public class AppDbContext : DbContext
    {
        // DbSet = une table dans la base de données
        // Chaque DbSet correspond à une entité
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la clé primaire composite de RecipeIngredient
            // (pas de Id simple — la clé = RecipeId + IngredientId ensemble)
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            // Configuration de la relation Recipe → RecipeIngredient
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
            // Cascade = si on supprime une recette, ses RecipeIngredients sont supprimés aussi

            // Configuration de la relation Ingredient → RecipeIngredient
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
            // Restrict = on ne peut pas supprimer un ingrédient s'il est utilisé dans une recette

            // Stocker les enums comme des strings dans la BDD (plus lisible)
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.CuisineType)
                .HasConversion<string>();
        
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomate", CaloriesPerUnit = 18, Unit = "100g", Description = "Tomate fraîche" },
                new Ingredient { Id = 2, Name = "Farine", CaloriesPerUnit = 364, Unit = "100g", Description = "Farine de blé" },
                new Ingredient { Id = 3, Name = "Oeuf", CaloriesPerUnit = 70, Unit = "pièce", Description = "Oeuf frais" },
                new Ingredient { Id = 4, Name = "Huile d'olive", CaloriesPerUnit = 120, Unit = "cuillère", Description = "Huile d'olive extra vierge" },
                new Ingredient { Id = 5, Name = "Poulet", CaloriesPerUnit = 165, Unit = "100g", Description = "Blanc de poulet" },
                new Ingredient { Id = 6, Name = "Riz", CaloriesPerUnit = 130, Unit = "100g", Description = "Riz blanc cuit" },
                new Ingredient { Id = 7, Name = "Lait", CaloriesPerUnit = 42, Unit = "100ml", Description = "Lait entier" },
                new Ingredient { Id = 8, Name = "Sucre", CaloriesPerUnit = 387, Unit = "100g", Description = "Sucre blanc" },
                new Ingredient { Id = 9, Name = "Concombre", CaloriesPerUnit = 12, Unit = "100g", Description = "Concombre frais" },
                new Ingredient { Id = 10, Name = "Fromage", CaloriesPerUnit = 402, Unit = "100g", Description = "Fromage à pâte dure" }    
            );
        }
    }
}