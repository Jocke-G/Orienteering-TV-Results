using System;
using System.Data;
using LayoutRestService.Dapper.Repositories;
using LayoutRestService.Models.Configuration;
using LayoutRestService.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace LayoutRestService.Dapper
{
    internal class Initializer
    {
        internal static void Initialize(IServiceCollection services, DatabaseConfiguration database)
        {
            Console.WriteLine("Initializing Dapper");
            services.AddScoped<IDbConnection>(x => new MySqlConnection(database.GetConnectionString()));

            services.AddScoped<ILayoutRepository, LayoutRepository>();
        }
    }
}
