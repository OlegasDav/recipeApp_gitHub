using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Repositories
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "recipes";

        public RecipesRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<ReadRecipe>> ShowAllAsync(Filter filter)
        {
            var sql = $"SELECT {TableName}.Id, {TableName}.Name, description.Description, {TableName}.Difficulty, {TableName}.TimeToComplete, {TableName}.DateCreated FROM {TableName} JOIN description on {TableName}.Id = description.id ORDER BY {filter.orderBy} {filter.orderHow}";

            return _sqlClient.QueryAsync<ReadRecipe>(sql);
        }

        public Task<int> AddAsync(Recipe recipe)
        {
            var sql = $"INSERT INTO {TableName} (Name, Difficulty, TimeToComplete, DateCreated) VALUES (@Name, @Difficulty, @TimeToComplete, @DateCreated);";

            return _sqlClient.ExecuteAsync(sql, new { 
                Name = recipe.Name,
                Difficulty = recipe.Difficulty.ToString(),
                TimeToComplete = recipe.TimeToComplete,
                DateCreated = recipe.DateCreated
            });
        }

        public Task<int> EditNameAsync(int id, string name)
        {
            var sql = $"UPDATE {TableName} SET Name = @Name WHERE Id = @Id;";

            return _sqlClient.ExecuteAsync(sql, new { Id = id, Name = name });
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
