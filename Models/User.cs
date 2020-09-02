using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public bool isAdmin { get; set; }
	}
}
