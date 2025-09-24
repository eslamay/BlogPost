using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IBlogImageRepository blogImageRepository;

		public ImagesController(IBlogImageRepository blogImageRepository)
		{
			this.blogImageRepository = blogImageRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllImages()
		{
			var blogImages = await blogImageRepository.GetAllAsync();
			var response = new List<BlogImageDto>();
			foreach (var blogImage in blogImages)
			{
				response.Add(new BlogImageDto
				{
					Id = blogImage.Id,
					DateCreated = blogImage.DateCreated,
					fileExtension = blogImage.fileExtension,
					fileName = blogImage.fileName,
					Title = blogImage.Title,
					Url = blogImage.Url
				});
			}
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
		{ 
			ValidateFileUpload(request.File);

			if (ModelState.IsValid)
			{
				var blogImage = new BlogImage
				{
					DateCreated = DateTime.Now,
					fileExtension = Path.GetExtension(request.File.FileName).ToLower(),
					fileName = request.FileName,
					Title = request.Title,
				};

				await blogImageRepository.Upload(request.File, blogImage);

				var response = new BlogImageDto
				{
					Id = blogImage.Id,
					DateCreated = blogImage.DateCreated,
					fileExtension = blogImage.fileExtension,
					fileName = blogImage.fileName,
					Title = blogImage.Title,
					Url= blogImage.Url
				};

				return Ok(response);
			}
			return BadRequest(ModelState);
		}

		private void ValidateFileUpload(IFormFile file)
		{
			var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
			if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
			{
				ModelState.AddModelError("file", "Unsupported file extension");
			}
			if (file.Length > 10485760)
			{
				ModelState.AddModelError("file", "File size must be less than 10 MB");
			}
		}
	}
}
