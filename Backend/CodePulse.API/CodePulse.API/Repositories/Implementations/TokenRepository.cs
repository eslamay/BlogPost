using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulse.API.Repositories.Implementations
{
	public class TokenRepository : ITokenRepository
	{
		private readonly IConfiguration configuration;

		public TokenRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public string CreateJwtToken(IdentityUser user, List<string> userRoles)
		{
			//create claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
			};

			claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));//add role claims
			//Jwt security token
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials
				);
			//return token

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
