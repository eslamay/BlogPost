using CodePulse.API.Data;
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
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories(
			[FromQuery] string? query,
			[FromQuery] string? sortBy,
			[FromQuery] string? sortDirection,
			[FromQuery] int? pageNumber,
			[FromQuery] int? pageSize)
		{
			var categories = await categoryRepository.GetAllCategories(query,sortBy,sortDirection,pageNumber,pageSize);

			var response = new List<CategoryDto>();

			foreach (var category in categories)
			{
				response.Add(new CategoryDto()
				{
					Id = category.Id,
					Name = category.Name,
					UrlHandle = category.UrlHandle
				});
			}

			return Ok(response);
		}

		[HttpGet]
		[Route("count")]
		public async Task<IActionResult> GetCategoriesCount()
		{
            var count = await categoryRepository.GetCategoryTotalCount();
			return Ok(count);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
		{
			var category = await categoryRepository.GetCategoryById(id);

			if (category == null)
			{
				return NotFound();
			}

			var response = new CategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}

		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto requestDto)
		{
			var category = new Category()
			{
				Name = requestDto.Name,
				UrlHandle = requestDto.UrlHandle
			};

			await categoryRepository.AddCategory(category);

			var response = new CategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}

		[HttpPut]
		[Route("{id:guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto requestDto)
		{
			var category = new Category()
			{
				Id = id,
				Name = requestDto.Name,
				UrlHandle = requestDto.UrlHandle
			};

			category = await categoryRepository.UpdateCategory(category);

			if (category == null)
			{
				return NotFound();
			}
			var response = new CategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};	
			return Ok(response);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
		{
			var category = await categoryRepository.DeleteCategory(id);

			if (category == null)
			{
				return NotFound();
			}

			var response = new CategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};
			return Ok(response);

		}
	}
}
