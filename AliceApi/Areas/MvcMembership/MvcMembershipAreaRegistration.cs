using System.Web.Mvc;

namespace AliceApi.Areas.MvcMembership
{
	public class MvcMembershipAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "MvcMembership";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"MvcMembership_default",
                "UserAdministration/{controller}/{action}/{id}",
				new {Controller = "UserAdministration", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
