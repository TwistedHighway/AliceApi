using System.Collections.Generic;
using System.Web.Security;
using AliceApi.Models;

namespace AliceApi.Areas.MvcMembership.Models.UserAdministration
{
	public class RoleViewModel
	{
		public string Role { get; set; }
		public IList<ApplicationUser> Users { get; set; }
	}
}
