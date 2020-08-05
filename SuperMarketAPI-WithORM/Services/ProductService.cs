using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> FindByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.FindByIdAsync(id);

                if (product == null)
                    return new ProductResponse("Product not found.");

                var category = await _categoryRepository.FindByIdAsync(product.CategoryId);

                product.Category = category;


                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when find the product: {ex.Message}");
            }
        }




        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (category == null)
                    return new ProductResponse("Category not found.");

                product.Category = category;

                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);

            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when save the product: {ex.Message}");
            }

        }


        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
             try {

                Product existingProduct = await _productRepository.FindByIdAsync(id);

                if(existingProduct == null){
                    return new ProductResponse("Product not found");
                }

                existingProduct.Name = product.Name;
                existingProduct.QuantityInPackage = product.QuantityInPackage;
                existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;


                Category newCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (newCategory == null)
                    return new ProductResponse("New Category not found");

                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Category = newCategory;

                await _productRepository.UpdateAsync(id, existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);

            }
            catch (Exception ex) {
                return new ProductResponse($"An error occurred when update the product: {ex.Message}");
            }
        }


        public async Task<ProductResponse> DeleteAsync(int id)
        {
            try
            {
                Product existingProduct = await _productRepository.FindByIdAsync(id);
                if (existingProduct == null)
                {
                    return new ProductResponse("Product not found");
                }
                existingProduct.Category = await _categoryRepository.FindByIdAsync(existingProduct.CategoryId);

                await _productRepository.DeleteAsync(existingProduct);
                await _unitOfWork.CompleteAsync();

                

                return new ProductResponse(existingProduct);

            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when delete the product: {ex.Message}");
            }


        }


    }
}