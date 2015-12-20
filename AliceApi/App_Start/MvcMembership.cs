using System.Web.Mvc;
using MvcMembership;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AliceApi.App_Start.MvcMembership), "Start")]
namespace AliceApi.App_Start
{
	public static class MvcMembership
	{
		public static void Start()
		{
			//GlobalFilters.Filters.Add(new TouchUserOnEachVisitFilter());
		}
	}
}
