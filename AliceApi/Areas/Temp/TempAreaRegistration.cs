using System.Web.Mvc;

namespace AliceApi.Areas.Temp
{
    public class TempAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Temp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Temp_default",
                "Temp/{controller}/{action}/{id}",
                new {Controller = "Temp", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
