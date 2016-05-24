using System.Collections.Generic;
using System.Web.Security;
using AliceApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace AliceApi.Areas.MvcMembership.Models.UserAdministration
{
	public class IndexViewModel
	{
		public string Search { get; set; }
		//public IPagedList<MembershipUser> Users { get; set; }
        public IPagedList<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
		public bool IsRolesEnabled { get; set; }
	}
}
