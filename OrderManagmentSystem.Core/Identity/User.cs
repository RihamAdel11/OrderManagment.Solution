using Microsoft.AspNetCore.Identity;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Entites
{
    public class User:IdentityUser
	{
		

		public string UserName { get; set; }
        public string PasswordHash { get; set; }
		public Role Role { get; set; }

    }
}
