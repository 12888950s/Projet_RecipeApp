# RecipeApp — Application de Gestion de Recettes Nutritionnelles

## À lire absolument avant de commencer

Ce README documente **l'ensemble du projet** — ce qu'a fait E1, ce que fait E2, et ce que fera E3.
Lisez votre section avant de commencer.

---

## Table des matières

1. [C'est quoi ce projet ?](#1-cest-quoi-ce-projet-)
2. [Architecture du projet](#2-architecture-du-projet)
3. [Ce que fait chaque étudiant](#3-ce-que-fait-chaque-étudiant)
4. [Ce qui a été fait par E1](#4-ce-qui-a-été-fait-par-e1-terminé)
5. [Ce qui a été fait par E2](#5-ce-qui-a-été-fait-par-e2-terminé)
6. [Ce que E3 doit faire](#6-ce-que-e3-doit-faire)
7. [Comment installer et lancer le projet](#7-comment-installer-et-lancer-le-projet)
8. [La base de données](#8-la-base-de-données)

---

## 1. C'est quoi ce projet ?

C'est une application web qui ressemble à **Marmiton** (site de recettes).
Elle permet de :

- Gérer une liste d'ingrédients avec leurs calories
- Créer des recettes avec ces ingrédients
- Calculer automatiquement les calories de chaque recette
- Afficher des statistiques sur un tableau de bord (graphiques)

**Technologies utilisées :**
- **C# / .NET 10** → le langage de programmation
- **Blazor** → pour les pages web (fait par E3)
- **Entity Framework Core 10** → pour la base de données
- **SQLite** → la base de données (un simple fichier `.db`)

---

## 2. Architecture du projet

```
RecipeApp/                              ← Le dossier principal (la solution)
│
├── RecipeApp.Data/                     ← COUCHE DATA (fait par E1) ✅
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── AppDbContextFactory.cs
│   ├── Entities/
│   │   ├── Ingredient.cs
│   │   ├── Recipe.cs
│   │   └── RecipeIngredient.cs
│   ├── Enums/
│   │   ├── Category.cs
│   │   └── CuisineType.cs
│   └── Migrations/
│
├── RecipeApp.Services/                 ← COUCHE SERVICES (E1 + E2) ✅
│   ├── Interfaces/
│   │   ├── IIngredientService.cs       ← E1 ✅
│   │   ├── IRecipeService.cs           ← E2 ✅
│   │   └── IAnalyticsService.cs        ← E2 ✅
│   └── Implementations/
│       ├── IngredientService.cs        ← E1 ✅
│       ├── RecipeService.cs            ← E2 ✅
│       └── AnalyticsService.cs         ← E2 ✅
│
└── RecipeApp.UI/                       ← COUCHE UI (fait par E3) ⏳
    ├── Pages/
    │   ├── Ingredients/
    │   │   ├── Index.razor
    │   │   └── Form.razor
    │   ├── Recipes/
    │   │   ├── Index.razor
    │   │   ├── Detail.razor
    │   │   └── Form.razor
    │   └── Dashboard.razor
    ├── Shared/
    │   ├── MainLayout.razor
    │   └── NavMenu.razor
    └── Program.cs
```

| Couche | Rôle | Qui ? |
|--------|------|-------|
| `RecipeApp.Data` | Parler à la base de données | E1 |
| `RecipeApp.Services` | La logique métier (calculs, règles) | E1 + E2 |
| `RecipeApp.UI` | Afficher les pages à l'utilisateur | E3 |

---

## 3. Ce que fait chaque étudiant

### Étudiant 1 (E1) — Data & Backend ✅ TERMINÉ
- Entités C# (Ingredient, Recipe, RecipeIngredient)
- DbContext + configuration Fluent API
- Migrations Code-First + Seed Data (10 ingrédients)
- Data Annotations (validation)
- IIngredientService + IngredientService (CRUD async + LINQ)

### Étudiant 2 (E2) — Services & Logique ✅ TERMINÉ
- IRecipeService + RecipeService (CRUD recettes async)
- Ajout / retrait / modification d'ingrédients dans une recette
- Calcul des calories totales et par personne
- IAnalyticsService + AnalyticsService (stats dashboard)
- Filtrage par catégorie et type de cuisine

### Étudiant 3 (E3) — UI Blazor ⏳ À FAIRE
> **Important : E3 doit attendre que E1 ET E2 aient terminé !**

- Configurer Program.cs (enregistrer tous les services)
- Pages Ingrédients (liste + formulaire)
- Pages Recettes (liste + détail + formulaire)
- Composant sélection d'ingrédients avec quantités
- Barre de recherche dynamique
- Page Dashboard avec graphiques (Chart.js)
- Navigation et layout général

---

## 4. Ce qui a été fait par E1 ✅ TERMINÉ

> Voir le README original d'E1 pour tous les détails.
> Résumé ci-dessous.

### Entités créées

| Entité | Champs principaux |
|--------|-------------------|
| `Ingredient` | Id, Name, CaloriesPerUnit, Unit, Description? |
| `Recipe` | Id, Title, Servings, Category, CuisineType, PrepTimeMinutes, CreatedAt |
| `RecipeIngredient` | RecipeId (FK), IngredientId (FK), Quantity, Notes? |

### Seed Data — 10 ingrédients disponibles

| Id | Nom | Calories | Unité |
|----|-----|----------|-------|
| 1 | Tomate | 18 | 100g |
| 2 | Farine | 364 | 100g |
| 3 | Oeuf | 70 | pièce |
| 4 | Huile d'olive | 120 | cuillère |
| 5 | Poulet | 165 | 100g |
| 6 | Riz | 130 | 100g |
| 7 | Lait | 42 | 100ml |
| 8 | Sucre | 387 | 100g |
| 9 | Concombre | 12 | 100g |
| 10 | Fromage | 402 | 100g |

### Méthodes disponibles — IIngredientService

```csharp
Task<List<Ingredient>> GetAllAsync();            // Tous les ingrédients triés A→Z
Task<Ingredient?> GetByIdAsync(int id);          // Un ingrédient par Id
Task<Ingredient> CreateAsync(Ingredient i);      // Ajouter un ingrédient
Task<Ingredient?> UpdateAsync(int id, ...);      // Modifier un ingrédient
Task<bool> DeleteAsync(int id);                  // Supprimer un ingrédient
Task<List<Ingredient>> SearchAsync(string s);    // Rechercher par nom
Task<List<Ingredient>> GetByUnitAsync(string u); // Filtrer par unité
```

---

## 5. Ce qui a été fait par E2 ✅ TERMINÉ

E2 a créé **4 fichiers** dans `RecipeApp.Services/` :

```
RecipeApp.Services/
├── Interfaces/
│   ├── IRecipeService.cs       ← Contrat du service recettes
│   └── IAnalyticsService.cs    ← Contrat du service analytique
└── Implementations/
    ├── RecipeService.cs        ← CRUD + calories + filtrage
    └── AnalyticsService.cs     ← Statistiques dashboard
```

---

### IRecipeService — Méthodes disponibles

```csharp
// ── CRUD de base ─────────────────────────────────────────────
Task<List<Recipe>> GetAllAsync();
Task<Recipe?> GetByIdAsync(int id);
Task<Recipe> CreateAsync(Recipe recipe);
Task<Recipe?> UpdateAsync(int id, Recipe recipe);
Task<bool> DeleteAsync(int id);

// ── Gestion des ingrédients dans une recette ─────────────────
Task<Recipe?> AddIngredientAsync(int recipeId, int ingredientId, float quantity, string? notes);
Task<bool> RemoveIngredientAsync(int recipeId, int ingredientId);
Task<bool> UpdateIngredientQuantityAsync(int recipeId, int ingredientId, float newQuantity);

// ── Calcul des calories ──────────────────────────────────────
Task<float> GetTotalCaloriesAsync(int recipeId);
Task<float> GetCaloriesPerPersonAsync(int recipeId);

// ── Filtrage et recherche ────────────────────────────────────
Task<List<Recipe>> GetByCategoryAsync(Category category);
Task<List<Recipe>> GetByCuisineTypeAsync(CuisineType cuisineType);
Task<List<Recipe>> SearchAsync(string searchTerm);
```

### Formule de calcul des calories

```csharp
// Calories totales d'une recette
var total = recipe.RecipeIngredients
    .Sum(ri => ri.Quantity * ri.Ingredient.CaloriesPerUnit / 100);

// Calories par personne
var perPerson = total / recipe.Servings;
```

---

### IAnalyticsService — Méthodes disponibles

```csharp
// Répartition par catégorie
Task<Dictionary<Category, int>>   GetRecipeCountByCategoryAsync();
Task<Dictionary<Category, float>> GetAvgCaloriesByCategoryAsync();

// Analyse par type de cuisine
Task<Dictionary<CuisineType, int>>   GetRecipeCountByCuisineAsync();
Task<Dictionary<CuisineType, float>> GetAvgCaloriesByCuisineAsync();

// Top recettes
Task<List<(string Title, float Calories)>> GetTopCaloricRecipesAsync(int count = 5);
Task<float> GetGlobalAverageCaloriesAsync();
```

---

### Comment utiliser les services d'E2 dans le code

**Injecter RecipeService :**
```csharp
@inject IRecipeService RecipeService
@inject IAnalyticsService AnalyticsService
```

**Créer une recette :**
```csharp
var recipe = await RecipeService.CreateAsync(new Recipe
{
    Title           = "Salade Tunisienne",
    Servings        = 4,
    Category        = Category.Lunch,
    CuisineType     = CuisineType.Tunisian,
    PrepTimeMinutes = 15
});
```

**Ajouter un ingrédient à la recette :**
```csharp
await RecipeService.AddIngredientAsync(recipe.Id, ingredientId: 1, quantity: 200, notes: "coupée en dés");
```

**Récupérer les calories :**
```csharp
var total     = await RecipeService.GetTotalCaloriesAsync(recipe.Id);
var perPerson = await RecipeService.GetCaloriesPerPersonAsync(recipe.Id);
```

**Filtrer par catégorie :**
```csharp
var lunches = await RecipeService.GetByCategoryAsync(Category.Lunch);
```

**Statistiques pour le dashboard :**
```csharp
var countByCategory = await AnalyticsService.GetRecipeCountByCategoryAsync();
var topRecipes      = await AnalyticsService.GetTopCaloricRecipesAsync(5);
var avgCalories     = await AnalyticsService.GetGlobalAverageCaloriesAsync();
```

---

## 6. Ce que E3 doit faire

> **Important : E3 doit attendre que E1 ET E2 aient terminé et partagé le projet !**

### Étape 1 — Configurer Program.cs

C'est la première chose à faire. Ouvrir `RecipeApp.UI/Program.cs` et ajouter :

```csharp
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Data;
using RecipeApp.Services.Implementations;
using RecipeApp.Services.Interfaces;

// Base de données
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=../RecipeApp.Data/recipeapp.db"));

// Services E1
builder.Services.AddScoped<IIngredientService, IngredientService>();

// Services E2
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
```

---

### Étape 2 — Pages Ingrédients

#### `Pages/Ingredients/Index.razor` — Liste des ingrédients

Ce que la page doit faire :
- Afficher tous les ingrédients dans un tableau (Nom, Calories, Unité)
- Avoir une barre de recherche qui filtre en temps réel (utiliser `SearchAsync`)
- Bouton "Ajouter un ingrédient" → va vers `Form.razor`
- Bouton "Modifier" sur chaque ligne → va vers `Form.razor?id=X`
- Bouton "Supprimer" sur chaque ligne → appelle `DeleteAsync` + confirmation

```csharp
// Méthodes d'E1 à utiliser dans cette page :
await IngredientService.GetAllAsync();
await IngredientService.SearchAsync(searchTerm);
await IngredientService.DeleteAsync(id);
```

#### `Pages/Ingredients/Form.razor` — Formulaire ajout/modification

Ce que la page doit faire :
- Si pas d'`id` en paramètre → formulaire de création
- Si `id` existe → formulaire de modification (pré-remplir les champs)
- Champs : Nom, Calories par unité, Unité, Description (optionnel)
- Bouton "Enregistrer" → appelle `CreateAsync` ou `UpdateAsync`
- Bouton "Annuler" → revient à la liste

```csharp
// Méthodes d'E1 à utiliser :
await IngredientService.GetByIdAsync(id);     // pour pré-remplir
await IngredientService.CreateAsync(ingredient);
await IngredientService.UpdateAsync(id, ingredient);
```

---

### Étape 3 — Pages Recettes

#### `Pages/Recipes/Index.razor` — Liste des recettes

Ce que la page doit faire :
- Afficher toutes les recettes (Titre, Catégorie, Cuisine, Personnes, Calories/personne)
- Filtre par catégorie (dropdown : Breakfast, Lunch, Dinner, Dessert, Snack)
- Filtre par type de cuisine (dropdown)
- Barre de recherche par titre
- Bouton "Voir" → va vers `Detail.razor?id=X`
- Bouton "Modifier" → va vers `Form.razor?id=X`
- Bouton "Supprimer" → appelle `DeleteAsync` + confirmation

```csharp
// Méthodes d'E2 à utiliser :
await RecipeService.GetAllAsync();
await RecipeService.SearchAsync(searchTerm);
await RecipeService.GetByCategoryAsync(category);
await RecipeService.GetByCuisineTypeAsync(cuisineType);
await RecipeService.DeleteAsync(id);
await RecipeService.GetCaloriesPerPersonAsync(id);
```

#### `Pages/Recipes/Detail.razor` — Détail d'une recette

Ce que la page doit faire :
- Afficher toutes les infos de la recette
- Lister tous les ingrédients avec leurs quantités et notes
- Afficher les calories totales et les calories par personne
- Bouton "Modifier la recette" → va vers `Form.razor?id=X`

```csharp
// Méthodes d'E2 à utiliser :
await RecipeService.GetByIdAsync(id);
await RecipeService.GetTotalCaloriesAsync(id);
await RecipeService.GetCaloriesPerPersonAsync(id);
```

#### `Pages/Recipes/Form.razor` — Formulaire création/modification

Ce que la page doit faire :
- Si pas d'`id` → formulaire de création
- Si `id` existe → formulaire de modification (pré-remplir)
- Champs : Titre, Description, Nombre de personnes, Catégorie, Type de cuisine, Temps de préparation
- Section "Ingrédients" : liste des ingrédients déjà ajoutés + bouton pour en ajouter
- Pour chaque ingrédient : afficher Nom, Quantité (modifiable), Notes, bouton Retirer
- Bouton "Enregistrer"

```csharp
// Méthodes d'E1 à utiliser (pour le sélecteur d'ingrédients) :
await IngredientService.GetAllAsync();

// Méthodes d'E2 à utiliser :
await RecipeService.GetByIdAsync(id);
await RecipeService.CreateAsync(recipe);
await RecipeService.UpdateAsync(id, recipe);
await RecipeService.AddIngredientAsync(recipeId, ingredientId, quantity, notes);
await RecipeService.RemoveIngredientAsync(recipeId, ingredientId);
await RecipeService.UpdateIngredientQuantityAsync(recipeId, ingredientId, newQty);
```

---

### Étape 4 — Page Dashboard

#### `Pages/Dashboard.razor`

Ce que la page doit afficher :
- Nombre total de recettes
- Calories moyennes globales par personne
- Graphique en camembert : répartition des recettes par catégorie
- Graphique en barres : calories moyennes par type de cuisine
- Tableau : Top 5 recettes les plus caloriques

```csharp
// Méthodes d'E2 à utiliser :
await AnalyticsService.GetRecipeCountByCategoryAsync();
await AnalyticsService.GetAvgCaloriesByCategoryAsync();
await AnalyticsService.GetRecipeCountByCuisineAsync();
await AnalyticsService.GetAvgCaloriesByCuisineAsync();
await AnalyticsService.GetTopCaloricRecipesAsync(5);
await AnalyticsService.GetGlobalAverageCaloriesAsync();
```

**Pour les graphiques, utiliser Chart.js :**

Ajouter dans `wwwroot/index.html` :
```html
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
```

Exemple d'appel JS depuis Blazor :
```csharp
await JS.InvokeVoidAsync("renderPieChart", "canvasId", labels, data);
```

---

### Étape 5 — Navigation

#### `Shared/NavMenu.razor`

Modifier la navigation pour avoir :
```
🏠 Accueil
🥕 Ingrédients      → /ingredients
📋 Recettes         → /recipes
📊 Dashboard        → /dashboard
```

---

### Résumé des fichiers à créer par E3

| Fichier | Rôle |
|---------|------|
| `Program.cs` | Enregistrer les services E1 + E2 |
| `Pages/Ingredients/Index.razor` | Liste + recherche + suppression |
| `Pages/Ingredients/Form.razor` | Création + modification |
| `Pages/Recipes/Index.razor` | Liste + filtres + recherche |
| `Pages/Recipes/Detail.razor` | Détail + calories |
| `Pages/Recipes/Form.razor` | Création + modification + gestion ingrédients |
| `Pages/Dashboard.razor` | Statistiques + graphiques Chart.js |
| `Shared/NavMenu.razor` | Navigation principale |

---

## 7. Comment installer et lancer le projet

### Prérequis
- .NET 10 SDK installé
- Visual Studio Code installé
- Git installé

### Étapes

**1. Cloner le projet :**
```bash
git clone https://github.com/VOTRE_USERNAME/RecipeApp.git
cd RecipeApp
```

**2. Restaurer les packages :**
```bash
dotnet restore
```

**3. Appliquer les migrations (créer la BDD) :**
```bash
dotnet ef database update --project RecipeApp.Data
```

**4. Lancer l'application :**
```bash
dotnet run --project RecipeApp.UI
```

---

## 8. La base de données

Le fichier `recipeapp.db` se trouve dans `RecipeApp.Data/`.

```
Table : Ingredients
- Id (INTEGER, PK, AUTO)
- Name (TEXT, NOT NULL)
- CaloriesPerUnit (REAL, NOT NULL)
- Unit (TEXT, NOT NULL)
- Description (TEXT, NULL)

Table : Recipes
- Id (INTEGER, PK, AUTO)
- Title (TEXT, NOT NULL)
- Description (TEXT, NULL)
- Servings (INTEGER, NOT NULL)
- Category (TEXT, NOT NULL)
- CuisineType (TEXT, NOT NULL)
- PrepTimeMinutes (INTEGER, NOT NULL)
- CreatedAt (TEXT, NOT NULL)

Table : RecipeIngredients
- RecipeId (INTEGER, FK → Recipes)     ← Cascade Delete
- IngredientId (INTEGER, FK → Ingredients)  ← Restrict Delete
- Quantity (REAL, NOT NULL)
- Notes (TEXT, NULL)
```

---

## Commandes utiles

```bash
# Compiler le projet
dotnet build

# Lancer l'application
dotnet run --project RecipeApp.UI

# Créer une nouvelle migration
dotnet ef migrations add NomMigration --project RecipeApp.Data

# Appliquer les migrations
dotnet ef database update --project RecipeApp.Data

# Annuler la dernière migration
dotnet ef migrations remove --project RecipeApp.Data
```

---

## Fait par

- **Étudiant 1** — Data & Backend (RecipeApp.Data + IIngredientService) — Mars 2026
- **Étudiant 2** — Services & Logique (IRecipeService + IAnalyticsService) — Mars 2026
- **Étudiant 3** — UI Blazor (RecipeApp.UI) — ⏳ En cours
- Technologies : .NET 10, Entity Framework Core 10, SQLite, Blazor, C#