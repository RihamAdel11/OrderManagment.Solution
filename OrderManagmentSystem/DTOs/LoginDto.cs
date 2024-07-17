﻿using System.ComponentModel.DataAnnotations;

namespace OrderManagmentSystem.DTOs
{
	public class LoginDto
	{
		[Required]
		public string Email { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}
