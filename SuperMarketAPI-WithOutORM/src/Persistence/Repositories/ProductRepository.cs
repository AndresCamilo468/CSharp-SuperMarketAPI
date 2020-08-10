using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        

        public async Task<IEnumerable<Product>> ListAsync()
        {
            NpgsqlConnection conection = await _context.OpenConection();


            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText =
                "SELECT " +
                    "p.id as id , " +
                    "p.name as name, " +
                    "p.quantityinpackage as quantityinpackage, " +
                    "p.unitofmeasurement as unitofmeasurement, " +
                    "c.id as categoryid, " +
                    "c.name as categoryname " +
                "FROM public.products as p " +
                "INNER JOIN public.categories as c ON p.categoryid = c.id;";


            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            List<Product> result = new List<Product>();

            while (reader.Read()) { 

                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                short quantityinpackage = reader.GetInt16(reader.GetOrdinal("quantityinpackage"));
                short unitofmeasurement = reader.GetInt16(reader.GetOrdinal("unitofmeasurement"));
                int categoryid = reader.GetInt32(reader.GetOrdinal("categoryid"));
                string categoryname = reader.GetString(reader.GetOrdinal("categoryname"));

                Category categoryAux = new Category(categoryid, categoryname);
                Product productAux = new Product(id,name, quantityinpackage, unitofmeasurement, categoryid, categoryAux);

                result.Add(productAux);
            }

            await conection.CloseAsync();

            return result;
        }


        public async Task<Product> FindByIdAsync(int id)
        {
            NpgsqlConnection conection = await _context.OpenConection();

            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText =
                "SELECT " +
                    "p.id as id , " +
                    "p.name as name, " +
                    "p.quantityinpackage as quantityinpackage, " +
                    "p.unitofmeasurement as unitofmeasurement, " +
                    "c.id as categoryid, " +
                    "c.name as categoryname " +
                "FROM public.products as p " +
                "INNER JOIN public.categories as c ON p.categoryid = c.id " +
                "WHERE p.id = @id;";


            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            Product result = null;

            if (reader.Read())
            {
                string name = reader.GetString(reader.GetOrdinal("name"));
                short quantityinpackage = reader.GetInt16(reader.GetOrdinal("quantityinpackage"));
                short unitofmeasurement = reader.GetInt16(reader.GetOrdinal("unitofmeasurement"));
                int categoryid = reader.GetInt32(reader.GetOrdinal("categoryid"));
                string categoryname = reader.GetString(reader.GetOrdinal("categoryname"));

                Category categoryAux = new Category(categoryid, categoryname);
                result = new Product(id, name, quantityinpackage, unitofmeasurement, categoryid, categoryAux);
            }

            await conection.CloseAsync();
            return result;
        }



        public async Task AddAsync(Product product)
        {
            NpgsqlConnection conection = await _context.OpenConection();


            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText = 
                "INSERT INTO " +
                "public.products(name, quantityinpackage, unitofmeasurement, categoryid) " +
                "VALUES " +
                "(@name, @quantityinpackage, @unitofmeasurement, @categoryid)" +
                "RETURNING id;";

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@quantityinpackage", product.QuantityInPackage);
            command.Parameters.AddWithValue("@unitofmeasurement", (short)product.UnitOfMeasurement);
            command.Parameters.AddWithValue("@categoryid", product.CategoryId);
            

            product.Id = (int)(await command.ExecuteScalarAsync());

            

            await conection.CloseAsync();

        }

        public async Task UpdateAsync(int id, Product product)
        {
            NpgsqlConnection conection = await _context.OpenConection();


            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText =
                "UPDATE public.products SET " +
                    "name = @name, quantityinpackage = @quantityinpackage, unitofmeasurement = @unitofmeasurement, categoryid = @categoryid " +
                "WHERE id = @id; ";
            
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@quantityinpackage", product.QuantityInPackage);
            command.Parameters.AddWithValue("@unitofmeasurement", (short)product.UnitOfMeasurement);
            command.Parameters.AddWithValue("@categoryid", product.CategoryId);


            await command.ExecuteNonQueryAsync();

            await conection.CloseAsync();
        }

        public async Task DeleteAsync(int id)
        {
            NpgsqlConnection conection = await _context.OpenConection();


            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText =
                "DELETE FROM public.products WHERE id = @id; ";

            command.Parameters.AddWithValue("@id", id);


            await command.ExecuteNonQueryAsync();

            await conection.CloseAsync();
        }
    }
    
}