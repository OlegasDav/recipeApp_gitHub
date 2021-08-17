using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Repositories
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<ReadRecipe>> ShowAllAsync(Filter filter);

        Task<int> AddAsync(Recipe recipe);

        Task<int> EditAsync(int id, string name);

        Task<int> DeleteAsync(int id);

        Task<int> DeleteAllAsync();
    }
}
