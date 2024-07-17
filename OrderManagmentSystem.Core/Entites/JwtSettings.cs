using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Entites
{
	public class JwtSettings
	{
		public string Secret { get; set; }
        public string From { get; set; }
		public string To { get; set; }
        public string Issuer { get; set; }
		public string Audience { get; set; }
		public int AccessTokenExpiration { get; set; }
		public int RefreshTokenExpiration { get; set; }
	}
}
