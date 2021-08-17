using Contracts.Enums;
using Domain.Models;
using Domain.Services;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    class RecipeApp
    {
        private readonly IRecipesService _recipesService;
        public RecipeApp(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        public async Task StartAsync()
        {
            int id;
            string name;
            var description = string.Empty;
            Difficulty difficulty;
            double minutes;


            while (true)
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("1 - Show all recipes");
                Console.WriteLine("2 - Add recipe");
                Console.WriteLine("3 - Edit recipe");
                Console.WriteLine("4 - Delete recipe");
                Console.WriteLine("5 - Delete all recipes");
                Console.WriteLine("6 - Exit");

                var chosenCommand = Console.ReadLine();

                switch (chosenCommand)
                {
                    case "1":
                        Console.WriteLine("Enter order by TimeToComplete or DateCreated: ");
                        var orderBy = Console.ReadLine();
                        Console.WriteLine("Enter ASC or DESC: ");
                        var orderHow = Console.ReadLine();

                        var allRecipes = await _recipesService.GetAllAsync(new Filter 
                        {
                            orderBy = orderBy,
                            orderHow = orderHow
                        });

                        foreach (var recipe in allRecipes)
                        {
                            Console.WriteLine(recipe.ToString());
                        }

                        break;
                    case "2":
                        //Console.WriteLine("Enter note id:");
                        //id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter recipe Name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter recipe Description: ");
                        description = Console.ReadLine();
                        Console.WriteLine("Enter recipe Difficulty (Easy, Medium, Hard): ");
                        var difficultyinput = Console.ReadLine();
                        Enum.TryParse(difficultyinput, out difficulty);
                        Console.WriteLine("Enter recipe Time To Complete (min): ");
                        minutes = Convert.ToDouble(Console.ReadLine());

                        await _recipesService.CreateAsync(new RecipeFull
                        {
                            Name = name,
                            Description = description,
                            DateCreated = DateTime.Now,
                            Difficulty = difficulty,
                            TimeToComplete = TimeSpan.FromMinutes(minutes)
                        });

                        break;
                    case "3":
                        Console.WriteLine("Enter recipe ID");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new recipe Name");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter new recipe Description");
                        description = Console.ReadLine();

                        await _recipesService.EditAsync(id, name, description);
                        break;
                    case "4":
                        Console.WriteLine("Enter recipe ID:");
                        id = Convert.ToInt32(Console.ReadLine());

                        await _recipesService.DeleteByIdAsync(id);
                        break;
                    case "5":
                        var recipesDeleted = await _recipesService.ClearAllAsync();
                        Console.WriteLine($"{recipesDeleted} recepes were deleted");
                        break;
                    case "6":
                        return;
                }
            }
        }
    }
}
