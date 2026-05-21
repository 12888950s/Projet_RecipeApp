using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Data;
using RecipeApp.Services.Implementations;
using RecipeApp.Services.Interfaces;
using RecipeApp.UI;

var builder = WebApplication.CreateBuilder(args);

// ─── Base de données SQLite ───────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=recipeapp.db"));

// ─── Services métier ──────────────────────────────────────────────
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// ─── Blazor ───────────────────────────────────────────────────────
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ─── Pipeline HTTP ────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();