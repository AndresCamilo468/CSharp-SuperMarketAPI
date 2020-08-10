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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            NpgsqlConnection conection = await _context.OpenConection();


            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText = "SELECT * FROM public.categories";
            
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            List<Category> result = new List<Category>();
            
            while (reader.Read()) {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Category categoryAux = new Category(id, name);
                result.Add(categoryAux);
            }

            await conection.CloseAsync();
            return result;
            
        }

        public async Task AddAsync(Category category)
        {

            NpgsqlConnection conection = await _context.OpenConection();

            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText = "INSERT INTO public.categories(name)VALUES(@name) RETURNING id;";
            command.Parameters.AddWithValue("@name", category.Name);

            category.Id = (int)(await command.ExecuteScalarAsync());
            await conection.CloseAsync();

        }



        public async Task<Category> FindByIdAsync(int id)
        {
            NpgsqlConnection conection = await _context.OpenConection();

            NpgsqlCommand command = conection.CreateCommand();
            command.CommandText = "SELECT * FROM public.categories WHERE id=@id";
            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            Category result = null;

            if (reader.Read()){
                string name = reader.GetString(1); ;
                result = new Category(id, name);
            }

            await conection.CloseAsync();
            return result;
        }




        public async void Update(Category category)
        {
            NpgsqlConnection conection = await _context.OpenConection();

            NpgsqlCommand command = conection.CreateCommand();

            command.CommandText = " UPDATE public.categories SET name = @name WHERE id = @id; ";
            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@id", category.Id);

            await command.ExecuteNonQueryAsync();
            await conection.CloseAsync();
        }



        public async void Remove(Category category)
        {
            NpgsqlConnection conection = await _context.OpenConection();

            NpgsqlCommand command = conection.CreateCommand();

            command.CommandText = " DELETE FROM public.categories WHERE id = @id;";
            command.Parameters.AddWithValue("@id", category.Id);

            await command.ExecuteNonQueryAsync();
            await conection.CloseAsync();
        }
    }
}