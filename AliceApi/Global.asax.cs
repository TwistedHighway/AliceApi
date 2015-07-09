using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using NLog;
using NLog.Config;

namespace AliceApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {

          protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // http://dotnetdarren.wordpress.com/2010/07/27/logging-on-mvc-part-1/
            // ELMAH - Global Error Handler 
            ControllerBuilder.Current.SetControllerFactory(new ErrorHandlingControllerFactory());
            // Register custom NLog Layout renderers
            //LayoutRendererFactory.AddLayoutRenderer("utc_date", typeof(Area51.Services.Logging.NLog.UtcDateRenderer));
            //LayoutRendererFactory.AddLayoutRenderer("web_variables", typeof(Area51.Services.Logging.NLog.WebVariablesRenderer));
            
            //For later versions of NLog (including v4.0.30319):
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("utc_date", typeof(AliceApi.Services.Logging.NLog.UtcDateRenderer));
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("web_variables", typeof(AliceApi.Services.Logging.NLog.WebVariablesRenderer));

            // some say to put this here but I can't get it to work well here and without it it doesn't work at all...
            log4net.Config.XmlConfigurator.Configure(); // ?? should we need this? 
         

        }
    }
}