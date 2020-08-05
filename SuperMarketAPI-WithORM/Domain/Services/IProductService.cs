using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();

        Task<ProductResponse> FindByIdAsync(int id);

        Task<ProductResponse> SaveAsync(Product product);

        Task<ProductResponse> UpdateAsync(int id, Product product);

        Task<ProductResponse> DeleteAsync(int id);
    }
}