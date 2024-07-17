using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Services;
using OrderManagmentSystem.DTOs;
using OrderManagmentSystem.Error;

namespace OrderManagmentSystem.Controllers
{
	
	public class UserController : ApiBaseController 
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthServices _authServices;
		private readonly IMapper _mapper;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthServices authServices, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authServices = authServices;
			_mapper = mapper;
		}
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			return Ok(new UserDto()

			{
				DisplayName = user.UserName,
				Email = user.Email,
				Token = await _authServices.CreateTokenAsync(user, _userManager)
			});


		}
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new User()
			{
				UserName = model.DisplayName,
				Email = model.Email
				,
				PhoneNumber = model.Phone
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				return BadRequest(new APIValidationErrorResponse()
				{
					Errors = result.Errors.Select(E => E.Description)
				});

			}
			return Ok(new UserDto()


			{
				DisplayName = user.UserName,
				Email = user.Email,
				Token = await _authServices.CreateTokenAsync(user, _userManager)
			});
		}

	}
}
