using Domain.Models;
using Persistence.Models;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IRecipeDescriptionRepository _recipeDescriptionRepository;

        public RecipesService(IRecipesRepository recipesRepository, IRecipeDescriptionRepository recipeDescriptionRepository)
        {
            _recipesRepository = recipesRepository;
            _recipeDescriptionRepository = recipeDescriptionRepository;
        }

        public async Task<IEnumerable<RecipeFull>> GetAllAsync(Filter filter)
        {
            var recipes = await _recipesRepository.ShowAllAsync(filter);

            var recipesList = new List<RecipeFull>();

            foreach (var recipe in recipes)
            {
                recipesList.Add(new RecipeFull
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Difficulty = recipe.Difficulty,
                    TimeToComplete = recipe.TimeToComplete,
                    DateCreated = recipe.DateCreated
                });
            }

            return recipesList;
        }

        public async Task<int> CreateAsync(RecipeFull recipeFull)
        {
            var recipe = new Recipe
            {
                //Id = recipeFull.Id,
                Name = recipeFull.Name,
                Difficulty = recipeFull.Difficulty,
                TimeToComplete = recipeFull.TimeToComplete,
                DateCreated = recipeFull.DateCreated
            };

            var description = new RecipeDescription
            {
                //Id = recipeFull.Id,
                Description = recipeFull.Description
            };

            var recipesTask = _recipesRepository.AddAsync(recipe);
            var descriptionsTask = _recipeDescriptionRepository.AddAsync(description);

            await Task.WhenAll(recipesTask, descriptionsTask);

            return await recipesTask;
        }

        public async Task<int> EditAsync(int id, string name, string description)
        {
            var editRecipeTask = _recipesRepository.EditAsync(id, name);
            var editDescriptionTask = _recipeDescriptionRepository.EditAsync(id, description);

            await Task.WhenAll(editRecipeTask, editDescriptionTask);

            return await editRecipeTask;
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            var deleteRecipeTask = _recipesRepository.DeleteAsync(id);
            var deleteDescriptionTask = _recipeDescriptionRepository.DeleteAsync(id);

            await Task.WhenAll(deleteRecipeTask, deleteDescriptionTask);

            return await deleteRecipeTask;
        }

        public async Task<int> ClearAllAsync()
        {
            var deleteRecipesTask = _recipesRepository.DeleteAllAsync();
            var deleteDescriptionsTask = _recipeDescriptionRepository.DeleteAllAsync();

            await Task.WhenAll(deleteRecipesTask, deleteDescriptionsTask);

            return await deleteRecipesTask;
        }
    }
}
