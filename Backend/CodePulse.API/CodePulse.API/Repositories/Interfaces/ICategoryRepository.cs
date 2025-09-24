using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		Task<Category> AddCategory(Category category);

		Task<IEnumerable<Category>> GetAllCategories(string? query = null,string? sortBy = null,string?sortDirection = null,int? pageNumber = 1,int? pageSize = 10);

		Task<int> GetCategoryTotalCount();
		Task<Category?> GetCategoryById(Guid id);

		Task<Category?> UpdateCategory(Category category);

		Task<Category?> DeleteCategory(Guid id);
	}
}
