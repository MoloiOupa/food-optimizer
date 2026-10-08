using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Recipe
    {
        public string Name { get; set; } = string.Empty;
        public int Feeds { get; set; }
        public Dictionary<string, int> Ingredients { get; set; } = new Dictionary<string, int>();
    }

    static int maxPeopleFed = 0;
    static Dictionary<string, int> bestCombination = new Dictionary<string, int>();

    static void Main()
    {
        var inventory = new Dictionary<string, int>
        {
            { "Cucumber", 2 },
            { "Olives", 2 },
            { "Lettuce", 3 },
            { "Meat", 6 },
            { "Tomato", 6 },
            { "Cheese", 8 },
            { "Dough", 10 }
        };

        var recipes = new List<Recipe>
        {
            new Recipe { Name = "Sandwich", Feeds = 1, Ingredients = new Dictionary<string, int> { { "Dough", 1 }, { "Lettuce", 1 }, { "Cucumber", 1 }, { "Cheese", 1 } } },
            new Recipe { Name = "Burger", Feeds = 1, Ingredients = new Dictionary<string, int> { { "Meat", 1 }, { "Dough", 2 }, { "Tomato", 1 }, { "Cheese", 1 } } },
            new Recipe { Name = "Pie", Feeds = 1, Ingredients = new Dictionary<string, int> { { "Meat", 2 }, { "Dough", 1 } } },
            new Recipe { Name = "Pasta", Feeds = 2, Ingredients = new Dictionary<string, int> { { "Dough", 2 }, { "Meat", 1 }, { "Cheese", 2 } } },
            new Recipe { Name = "Salad", Feeds = 3, Ingredients = new Dictionary<string, int> { { "Lettuce", 2 }, { "Tomato", 1 }, { "Olives", 1 } } },
            new Recipe { Name = "Pizza", Feeds = 4, Ingredients = new Dictionary<string, int> { { "Dough", 3 }, { "Tomato", 2 }, { "Cheese", 3 }, { "Olives", 1 } } }
        };

        var currentCombination = recipes.ToDictionary(r => r.Name, r => 0);
        FindOptimalCombination(0, inventory, recipes, currentCombination, 0);

        Console.WriteLine($"Maximum people fed: {maxPeopleFed}");
        Console.WriteLine("Optimal combination:");
        foreach (var kvp in bestCombination)
        {
            if (kvp.Value > 0)
            {
                Console.WriteLine($"- {kvp.Value} x {kvp.Key}");
            }
        }
    }

    static void FindOptimalCombination(int recipeIndex, Dictionary<string, int> currentInventory, List<Recipe> recipes, Dictionary<string, int> currentCombination, int currentPeopleFed)
    {
        if (currentPeopleFed > maxPeopleFed)
        {
            maxPeopleFed = currentPeopleFed;
            bestCombination = new Dictionary<string, int>(currentCombination);
        }

        if (recipeIndex >= recipes.Count) return;

        Recipe recipe = recipes[recipeIndex];

        int count = 0;
        while (CanMake(recipe, currentInventory))
        {
            DeductIngredients(recipe, currentInventory);
            count++;
            currentCombination[recipe.Name] = count;

            FindOptimalCombination(recipeIndex + 1, currentInventory, recipes, currentCombination, currentPeopleFed + (count * recipe.Feeds));
        }

        while (count > 0)
        {
            AddIngredients(recipe, currentInventory);
            count--;
        }
        currentCombination[recipe.Name] = 0;

        FindOptimalCombination(recipeIndex + 1, currentInventory, recipes, currentCombination, currentPeopleFed);
    }

    static bool CanMake(Recipe recipe, Dictionary<string, int> inventory)
    {
        foreach (var req in recipe.Ingredients)
        {
            if (!inventory.ContainsKey(req.Key) || inventory[req.Key] < req.Value)
                return false;
        }
        return true;
    }

    static void DeductIngredients(Recipe recipe, Dictionary<string, int> inventory)
    {
        foreach (var req in recipe.Ingredients)
        {
            inventory[req.Key] -= req.Value;
        }
    }

    static void AddIngredients(Recipe recipe, Dictionary<string, int> inventory)
    {
        foreach (var req in recipe.Ingredients)
        {
            inventory[req.Key] += req.Value;
        }
    }
}