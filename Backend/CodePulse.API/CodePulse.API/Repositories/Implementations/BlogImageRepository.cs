using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
	public class BlogImageRepository : IBlogImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ApplicationDbContext dbContext;

		public BlogImageRepository(IWebHostEnvironment webHostEnvironment,
			IHttpContextAccessor httpContextAccessor,ApplicationDbContext dbContext)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<BlogImage>> GetAllAsync()
		{
			return await dbContext.BlogImages.ToListAsync();
		}

		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
		{
			//1-upload images to api
			var localPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", $"{blogImage.fileName}{ blogImage.fileExtension}");
			using var stream=new FileStream(localPath, FileMode.Create);
			await file.CopyToAsync(stream);

			//2-update db
			var httpRequest = httpContextAccessor.HttpContext.Request;
			var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}/Images/{blogImage.fileName}{blogImage.fileExtension}";
			blogImage.Url = urlPath;
			
			await dbContext.AddAsync(blogImage);
			await dbContext.SaveChangesAsync();

			return blogImage;
		}
	}
}
