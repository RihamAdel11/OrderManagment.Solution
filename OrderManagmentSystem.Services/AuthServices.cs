using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Services
{
	public class AuthServices : IAuthServices
	{
		private readonly IConfiguration _configuration;

		public AuthServices(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		
		
			
		public async Task<string> CreateTokenAsync(User User, UserManager<User> userManager)
		{

			var Authclaim = new List<Claim>() { new Claim (ClaimTypes.GivenName ,User.UserName),
			new Claim (ClaimTypes .Email,User.Email )};
			var UserRoles = await userManager.GetRolesAsync(User);
			foreach(var Role in UserRoles)
			{
				Authclaim.Add(new Claim(ClaimTypes.Role, Role));
			}
			var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty));
			var token = new JwtSecurityToken(

				audience: _configuration["JWT:ValidAudience"],
				issuer: _configuration["JWT:ValidIssuser"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
				claims: Authclaim,
				signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature));
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		}
	}

