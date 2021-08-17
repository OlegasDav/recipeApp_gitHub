using Domain.Models;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IRecipesService
    {
        Task<IEnumerable<RecipeFull>> GetAllAsync(Filter filter);

        Task<int> CreateAsync(RecipeFull recipeFull);

        Task<int> EditAsync(int id, string name, string description);

        Task<int> DeleteByIdAsync(int id);

        Task<int> ClearAllAsync();
    }
}
