using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly ApplicationDbContext dbContext;

		public BlogPostRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<BlogPost> AddAsync(BlogPost blogPost)
		{
			await dbContext.BlogPosts.AddAsync(blogPost);
			await dbContext.SaveChangesAsync();
			return blogPost;
		}

		public async Task<BlogPost?> DeleteAsync(Guid id)
		{
			var existingBlogPost = await dbContext.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.Id == id);

			if (existingBlogPost == null)
			{
				return null;
			}

			dbContext.BlogPosts.Remove(existingBlogPost);
			await dbContext.SaveChangesAsync();

			return existingBlogPost;
		}

		public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
		{
			return await dbContext.BlogPosts.Include(bp => bp.Categories).ToListAsync();
		}

		public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
		{  
             return await dbContext.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.Id == id);
		}

		public async Task<BlogPost?> GetBlogPostByUrlHandleAsync(string urlHandle)
		{
			return await dbContext.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.UrlHandle == urlHandle);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await dbContext.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);

			if (existingBlogPost == null)
			{
				return null;
			}

			dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
			existingBlogPost.Categories= blogPost.Categories;

			await dbContext.SaveChangesAsync();
			return existingBlogPost;
		}
	}
}
