using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext dbContext;

		public CategoryRepository(ApplicationDbContext dbContext) 
		{
			this.dbContext = dbContext;
		}

		public async Task<Category> AddCategory(Category category)
		{
			await dbContext.AddAsync(category);
			await dbContext.SaveChangesAsync();
			return category;
		}

		public async Task<Category?> DeleteCategory(Guid id)
		{
			var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

			if (existingCategory == null) {
                  return null;
			}

			dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
			return existingCategory;
		}

		public async Task<IEnumerable<Category>> GetAllCategories(
			string? query=null,
			string? sortBy = null,
			string? sortDirection = null,
			int? pageNumber = 1,
			int?pageSize = 10)
		{
			var categories = dbContext.Categories.AsQueryable();
			//filter
			if (!string.IsNullOrEmpty(query))
			{
				categories = categories.Where(x => x.Name.Contains(query));
			}
			//sort
			if (!string.IsNullOrEmpty(sortBy))
			{
				if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
				{
					var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase)? true : false;

					categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
				}

				if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
				{
					var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

					categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.UrlHandle);
				}
			}

			//pagination
			var skip = (pageNumber - 1) * pageSize;

			categories = categories.Skip(skip??0).Take(pageSize??10);

			return await categories.ToListAsync();
		}

		public async Task<Category?> GetCategoryById(Guid id)
		{
             return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<int> GetCategoryTotalCount()
		{
			return await dbContext.Categories.CountAsync();
		}

		public async Task<Category?> UpdateCategory(Category category)
		{
			 var existingCategory =await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
			if (existingCategory == null)
			{
				return null;
			}

			existingCategory.Name = category.Name;
			existingCategory.UrlHandle = category.UrlHandle;

			await dbContext.SaveChangesAsync();
			return existingCategory;
		}
	}
}
