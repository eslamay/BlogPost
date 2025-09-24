using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;
		private readonly ICategoryRepository categoryRepository;

		public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
		{
			this.blogPostRepository = blogPostRepository;
			this.categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBlogPosts()
		{
			var blogPosts = await blogPostRepository.GetAllBlogPostsAsync();

			var response = new List<BlogPostDto>();

			foreach (var blogPost in blogPosts)
			{
				response.Add(new BlogPostDto()
				{
					Id = blogPost.Id,
					Name = blogPost.Name,
					UrlHandle = blogPost.UrlHandle,
					Content = blogPost.Content,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					ShortDescription = blogPost.ShortDescription,
					IsVisible = blogPost.IsVisible,
					PublishedDate = blogPost.PublishedDate,
					Categories = blogPost.Categories.Select(c => new CategoryDto
					{ Id = c.Id, Name = c.Name, UrlHandle = c.UrlHandle }).ToList()
				});
			}

			return Ok(response);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
		{
			var blogPost = await blogPostRepository.GetBlogPostByIdAsync(id);

			if (blogPost == null)
			{
				return NotFound();
			}

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Name = blogPost.Name,
				UrlHandle = blogPost.UrlHandle,
				Content = blogPost.Content,
				Author = blogPost.Author,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				ShortDescription = blogPost.ShortDescription,
				IsVisible = blogPost.IsVisible,
				PublishedDate = blogPost.PublishedDate,
				Categories = blogPost.Categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					UrlHandle = c.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		[HttpGet]
		[Route("{urlHandle}")]
		public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
		{
			var blogPost = await blogPostRepository.GetBlogPostByUrlHandleAsync(urlHandle);

			if (blogPost == null)
			{
				return NotFound();
			}
			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Name = blogPost.Name,
				UrlHandle = blogPost.UrlHandle,
				Content = blogPost.Content,
				Author = blogPost.Author,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				ShortDescription = blogPost.ShortDescription,
				IsVisible = blogPost.IsVisible,
				PublishedDate = blogPost.PublishedDate,
				Categories = blogPost.Categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					UrlHandle = c.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> CreateBlogPost([FromBody] AddBlogPostRequestDto request)
		{
			var blogPost = new BlogPost()
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle,
				Content = request.Content,
				Author = request.Author,
				FeaturedImageUrl = request.FeaturedImageUrl,
				ShortDescription = request.ShortDescription,
				IsVisible = request.IsVisible,
				PublishedDate = request.PublishedDate,
				Categories = new List<Category>()
			};

			foreach (var categoryId in request.CategoryId)
			{
				var existcategory = await categoryRepository.GetCategoryById(categoryId);
				if (existcategory != null)
				{
					blogPost.Categories.Add(existcategory);
				}
			}


			await blogPostRepository.AddAsync(blogPost);

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Name = blogPost.Name,
				UrlHandle = blogPost.UrlHandle,
				Content = blogPost.Content,
				Author = blogPost.Author,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				ShortDescription = blogPost.ShortDescription,
				IsVisible = blogPost.IsVisible,
				PublishedDate = blogPost.PublishedDate,
				Categories = blogPost.Categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					UrlHandle = c.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		[HttpPut]
		[Route("{id:guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
		{
			var blogPost = new BlogPost()
			{
				Id = id,
				Name = request.Name,
				UrlHandle = request.UrlHandle,
				Content = request.Content,
				Author = request.Author,
				FeaturedImageUrl = request.FeaturedImageUrl,
				ShortDescription = request.ShortDescription,
				IsVisible = request.IsVisible,
				PublishedDate = request.PublishedDate,
				Categories = new List<Category>()
			};

			foreach (var categoryId in request.CategoryId)
			{
				var existcategory = await categoryRepository.GetCategoryById(categoryId);
				if (existcategory != null)
				{
					blogPost.Categories.Add(existcategory);
				}
			}

		    var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

			if (updatedBlogPost == null)
			{
				return NotFound();
			}

			var response = new BlogPostDto()
			{
				Id = updatedBlogPost.Id,
				Name = updatedBlogPost.Name,
				UrlHandle = updatedBlogPost.UrlHandle,
				Content = updatedBlogPost.Content,
				Author = updatedBlogPost.Author,
				FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
				ShortDescription = updatedBlogPost.ShortDescription,
				IsVisible = updatedBlogPost.IsVisible,
				PublishedDate = updatedBlogPost.PublishedDate,
				Categories = updatedBlogPost.Categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					UrlHandle = c.UrlHandle
				}).ToList()
			};

			return Ok(response);

		}

		[HttpDelete]
		[Route("{id:guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
		{
			var deletedBlogPost = await blogPostRepository.DeleteAsync(id);

			if (deletedBlogPost == null)
			{
				return NotFound();
			}
			var response = new BlogPostDto()
			{
				Id = deletedBlogPost.Id,
				Name = deletedBlogPost.Name,
				UrlHandle = deletedBlogPost.UrlHandle,
				Content = deletedBlogPost.Content,
				Author = deletedBlogPost.Author,
				FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
				ShortDescription = deletedBlogPost.ShortDescription,
				IsVisible = deletedBlogPost.IsVisible,
				PublishedDate = deletedBlogPost.PublishedDate,
				Categories = deletedBlogPost.Categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					UrlHandle = c.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}
	}
}
