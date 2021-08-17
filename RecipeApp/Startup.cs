using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Persistence;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            AddSql(services);

            services.AddSingleton<IRecipesRepository, RecipesRepository>();
            services.AddSingleton<IRecipeDescriptionRepository, RecipeDescriptionRepository>();
            services.AddSingleton<IRecipesService, RecipesService>();
            services.AddSingleton<RecipeApp>();

            return services.BuildServiceProvider();
        }

        private IServiceCollection AddSql(IServiceCollection services)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();

            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "userOleg";
            connectionStringBuilder.Password = "rootroot";
            connectionStringBuilder.Database = "recipeapp";

            var connectionString = connectionStringBuilder.GetConnectionString(true);

            services.AddTransient<ISqlClient>(_ => new SqlClient(connectionString));

            return services;
        }
    }
}
