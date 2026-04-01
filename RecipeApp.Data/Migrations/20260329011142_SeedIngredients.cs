using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CaloriesPerUnit", "Description", "Name", "Unit" },
                values: new object[,]
                {
                    { 1, 18f, "Tomate fraîche", "Tomate", "100g" },
                    { 2, 364f, "Farine de blé", "Farine", "100g" },
                    { 3, 70f, "Oeuf frais", "Oeuf", "pièce" },
                    { 4, 120f, "Huile d'olive extra vierge", "Huile d'olive", "cuillère" },
                    { 5, 165f, "Blanc de poulet", "Poulet", "100g" },
                    { 6, 130f, "Riz blanc cuit", "Riz", "100g" },
                    { 7, 42f, "Lait entier", "Lait", "100ml" },
                    { 8, 387f, "Sucre blanc", "Sucre", "100g" },
                    { 9, 12f, "Concombre frais", "Concombre", "100g" },
                    { 10, 402f, "Fromage à pâte dure", "Fromage", "100g" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
