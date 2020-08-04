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
        readonly string DataBase = "Host=ec2-18-206-202-208.compute-1.amazonaws.com;Port=5432;Username=postgres;Password=12345678;Database=super_market_db";

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