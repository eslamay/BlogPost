using CodePulse.API.Models;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;

		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

			if (user != null)
			{
				var checkPassword =await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

				if (checkPassword)
				{
					var userRoles = await userManager.GetRolesAsync(user);

					var authToken = tokenRepository.CreateJwtToken(user, userRoles.ToList());

					var response = new LoginResponseDto()
					{
						Email = user.Email,
						Token = authToken,
						Roles = userRoles.ToList()
					};

					return Ok(response);
				}
			}

			ModelState.AddModelError("Login", "Invalid Email or Password");

			return ValidationProblem(ModelState);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
		{
			var user = new IdentityUser()
			{
				UserName = requestDto.UserName,
				Email = requestDto.Email?.Trim(),
				EmailConfirmed = true
			};
			
			var result = await userManager.CreateAsync(user, requestDto.Password);

			if (result.Succeeded)
			{
				result = await userManager.AddToRoleAsync(user, "Reader");

				if (result.Succeeded)
				{
					return Ok(result);
				}
				else
				{
					if (result.Errors.Any())
					{
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(error.Code, error.Description);
						}
					}
				}
			}
			else
			{
				if(result.Errors.Any())
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}
			}

			return ValidationProblem(ModelState);
		}
	}
}
