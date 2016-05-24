using System.Collections.Generic;
using System.Web.Security;
using AliceApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AliceApi.Areas.MvcMembership.Models.UserAdministration
{
	public class DetailsViewModel
	{
		#region StatusEnum enum

		public enum StatusEnum
		{
			Offline,
			Online,
			LockedOut,
			Unapproved,
            Unknown,
            SkiingAlps,
            FlyingAlaska,
            DivingMaldives
		}

		#endregion

		public string DisplayName { get; set; }
		public StatusEnum Status { get; set; }
		public ApplicationUser User { get; set; }
		public bool CanResetPassword { get; set; }
		public bool RequirePasswordQuestionAnswerToResetPassword { get; set; }
		public IList<IdentityUserRole> Roles { get; set; }
        public Dictionary<string, string> Rolls { get; set; }
		public bool IsRolesEnabled { get; set; }
	}
}
