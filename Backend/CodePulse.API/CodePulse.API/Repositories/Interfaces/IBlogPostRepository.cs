using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interfaces
{
	public interface IBlogPostRepository
	{
		Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost?> GetBlogPostByIdAsync(Guid id);
		Task<BlogPost?> GetBlogPostByUrlHandleAsync(string urlHandle);
		Task<BlogPost> AddAsync(BlogPost blogPost);

		Task<BlogPost?> UpdateAsync(BlogPost blogPost);

		Task<BlogPost?> DeleteAsync(Guid id);
		
	}
}
