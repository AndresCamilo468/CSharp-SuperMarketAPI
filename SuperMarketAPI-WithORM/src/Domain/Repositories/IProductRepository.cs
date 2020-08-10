using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();

        Task<Product> FindByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(int id, Product product);

        Task DeleteAsync(Product product);
    }
}
