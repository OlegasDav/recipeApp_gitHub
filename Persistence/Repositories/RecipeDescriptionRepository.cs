using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RecipeDescriptionRepository : IRecipeDescriptionRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "description";

        public RecipeDescriptionRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<int> AddAsync(RecipeDescription recipeDescription)
        {
            var sql = $"INSERT INTO {TableName} (Description) VALUES (@Description);";

            return _sqlClient.ExecuteAsync(sql, recipeDescription);
        }

        public Task<int> EditAsync(int id, string description)
        {
            var sql = $"UPDATE {TableName} SET Description = @Description WHERE Id = @Id;";

            return _sqlClient.ExecuteAsync(sql, new { Id = id, Description = description });
        }

        public Task<int> DeleteAsync(int id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id;";

            return _sqlClient.ExecuteAsync(sql, new { Id = id });
        }

        public Task<int> DeleteAllAsync()
        {
            var sql = $"DELETE FROM {TableName};";

            return _sqlClient.ExecuteAsync(sql);
        }
    }
}
