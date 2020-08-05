using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

using Npgsql;

namespace Supermarket.API.Persistence.Contexts
{
    public class AppDbContext
    {
        


        readonly string DataBase = $"Host={Environment.GetEnvironmentVariable("HOST")};" +
                                   $"Port={Environment.GetEnvironmentVariable("PORT")};" +
                                   $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                                   $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                                   $"Database={Environment.GetEnvironmentVariable("DB_NAME")}";

        public async Task<NpgsqlConnection> OpenConection(){

            NpgsqlConnection connection = new NpgsqlConnection();
            

            try{
                connection = new NpgsqlConnection(DataBase);
                await connection.OpenAsync();

            }
            catch (Exception){
                await connection.CloseAsync();
            }
            return connection;
        }
    }
}