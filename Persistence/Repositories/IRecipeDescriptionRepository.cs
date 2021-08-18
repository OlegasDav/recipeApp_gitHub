using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IRecipeDescriptionRepository
    {
        Task<int> AddAsync(RecipeDescription recipeDescription);

        Task<int> EditDescriptionAsync(int id, string description);

        Task<int> DeleteAsync(int id);

        Task<int> DeleteAllAsync();
    }
}
