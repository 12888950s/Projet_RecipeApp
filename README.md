# RecipeApp - Système de Gestion de Recettes Nutritionnelles

## Description du Projet

**RecipeApp** est une plateforme web moderne conçue pour simplifier la gestion culinaire et le suivi nutritionnel. L'application permet aux utilisateurs de centraliser leurs ingrédients, de concevoir des recettes détaillées et d'obtenir des analyses précises sur les apports caloriques. 

L'objectif principal est d'offrir une expérience utilisateur fluide tout en garantissant une précision technique dans le calcul des données nutritionnelles. Que ce soit pour un usage domestique ou professionnel, RecipeApp automatise les tâches complexes comme le calcul des calories par portion et la répartition statistique des types de cuisine.

---

## Architecture Technique

Le projet adopte une architecture en couches (N-Tier) afin de garantir une séparation nette des préoccupations, facilitant ainsi la maintenance et l'évolution du système :

1.  **RecipeApp.Data (Couche de Données) :** Gère l'accès aux données via Entity Framework Core. Elle contient les entités (Ingredients, Recipes, RecipeIngredients), les énumérations et la configuration du contexte SQLite.
2.  **RecipeApp.Services (Couche Métier) :** Contient toute la logique transactionnelle. Elle assure les calculs nutritionnels complexes, le filtrage avancé et les agrégations de données pour les rapports analytiques.
3.  **RecipeApp.UI (Couche Présentation) :** Développée avec Blazor WebAssembly, cette interface réactive permet une interaction en temps réel avec l'utilisateur, intégrant des composants dynamiques et des visualisations graphiques.

---

## Structure des Fichiers

Voici une vue d'ensemble de l'organisation du code source :

```text
C:\Users\TUF\Projet_RecipeApp\
├── RecipeApp.Data\                 # Persistence et Modèles de données
│   ├── Data\                       # Contexte de base de données (EF Core)
│   ├── Entities\                   # Classes POCO (Recipe, Ingredient, etc.)
│   ├── Enums\                      # Types énumérés (Catégories, Cuisines)
│   └── Migrations\                 # Historique des versions de la BDD
├── RecipeApp.Services\             # Logique Métier (Backend)
│   ├── Interfaces\                 # Contrats des services (Abstractions)
│   └── Implementations\            # Logique concrète (Calculs, Analytics)
└── RecipeApp.UI\                   # Interface Utilisateur (Blazor)
    ├── Pages\                      # Composants Razor (Vues de l'application)
    │   ├── Ingredients\            # Gestion des ingrédients
    │   ├── Recipes\                # Gestion des recettes
    │   └── Dashboard.razor         # Visualisation des statistiques
    ├── Shared\                     # Composants partagés (Layout, NavMenu)
    └── wwwroot\                    # Fichiers statiques (CSS, JS, Images)
```

### Détails des répertoires :
*   **Data/** : Contient le `AppDbContext` qui définit comment les objets sont mappés vers la base SQLite.
*   **Entities/** : Définit la structure des objets métier. La classe `RecipeIngredient` sert de table de liaison pour la relation plusieurs-à-plusieurs.
*   **Implementations/** : C'est ici que se trouve le "cerveau" de l'application, notamment les algorithmes de calcul de calories.
*   **Pages/** : Chaque fichier `.razor` correspond à une route de l'application web.

---

## Fonctionnalités Principales

### Gestion des Ingrédients
*   Inventaire complet avec noms, descriptions et calories par unité.
*   Recherche dynamique et filtrage par unité de mesure.
*   Interface CRUD (Création, Lecture, Mise à jour, Suppression) optimisée.

### Conception de Recettes
*   Éditeur de recettes intuitif permettant l'ajout d'ingrédients avec quantités spécifiques.
*   Calcul automatique des calories totales et des calories par personne.
*   Classification par catégories (Petit-déjeuner, Déjeuner, Dîner, etc.) et types de cuisine (Française, Tunisienne, Italienne, etc.).

### Tableau de Bord & Statistiques
*   Visualisation de la répartition des recettes par catégorie via des graphiques circulaires.
*   Analyse des calories moyennes par type de cuisine.
*   Classement des recettes les plus nutritives.
*   Intégration de **Chart.js** pour des rendus visuels professionnels.

---

## Technologies & Outils

*   **Framework :** .NET 10
*   **Frontend :** Blazor (C# / Razor Components)
*   **ORM :** Entity Framework Core 10
*   **Base de Données :** SQLite (Légère et portable)
*   **Visualisation :** Chart.js & JavaScript Interop
*   **Design :** Vanilla CSS (Responsive Design)

---

## Installation & Utilisation

### Prérequis
*   SDK .NET 10
*   Outil CLI Entity Framework (`dotnet ef`)

### Lancement du projet
1.  **Clonage du dépôt :**
    ```bash
    git clone https://github.com/12888950s/Projet_RecipeApp.git
    cd Projet_RecipeApp
    ```
2.  **Initialisation de la base de données :**
    ```bash
    dotnet ef database update --project RecipeApp.Data
    ```
3.  **Exécution de l'application :**
    ```bash
    dotnet run --project RecipeApp.UI
    ```

---

## Conclusion

RecipeApp démontre la puissance de l'écosystème .NET pour le développement d'applications web robustes et scalables. En combinant la rigueur d'un backend typé avec la flexibilité d'une interface Blazor, ce projet offre une solution complète pour la gestion culinaire moderne. L'architecture modulaire mise en place permet d'envisager sereinement l'ajout futur de fonctionnalités telles que la planification de menus hebdomadaires ou l'exportation de rapports nutritionnels en PDF.

---
*Développé dans le cadre d'un projet académique collaboratif — Mai 2026.*
