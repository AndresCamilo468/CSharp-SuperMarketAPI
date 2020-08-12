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
        string DataBase = "null";

        public AppDbContext(string DataBase)
        {
            this.DataBase = DataBase;
        
        }




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