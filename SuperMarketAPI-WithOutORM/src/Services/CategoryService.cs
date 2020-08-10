using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
			
		}

		public async Task<IEnumerable<Category>> ListAsync()
		{
			return await _categoryRepository.ListAsync();
		}

		public async Task<CategoryResponse> SaveAsync(Category category)
		{
			try
			{
				await _categoryRepository.AddAsync(category);
				
				return new CategoryResponse(category);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
			}
		}

		public async Task<CategoryResponse> UpdateAsync(int id, Category category)
		{
			var existingCategory = await _categoryRepository.FindByIdAsync(id);

			if (existingCategory == null)
				return new CategoryResponse("Category not found.");

			existingCategory.Name = category.Name;

			try
			{
				_categoryRepository.Update(existingCategory);
				

				return new CategoryResponse(existingCategory);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new CategoryResponse($"An error occurred when updating the category: {ex.Message}");
			}
		}

		public async Task<CategoryResponse> DeleteAsync(int id)
		{
			var existingCategory = await _categoryRepository.FindByIdAsync(id);

			if (existingCategory == null)
				return new CategoryResponse("Category not found.");

			try
			{
				_categoryRepository.Remove(existingCategory);
				

				return new CategoryResponse(existingCategory);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
			}
		}



        public async Task<CategoryResponse> FindByIdAsync(int id)
        {
			try
			{
				var category = await _categoryRepository.FindByIdAsync(id);

				if (category == null)
					return new CategoryResponse("Category not found.");


				return new CategoryResponse(category);
			}
			catch (Exception ex)
			{
				return new CategoryResponse($"An error occurred when find the category: {ex.Message}");
			}
		}
    }
}