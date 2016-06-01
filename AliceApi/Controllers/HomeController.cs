using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AliceApi.Services.Logging.Log4Net;
using AliceApi.Services.Logging.NLog;


namespace AliceApi.Controllers
{
    public class HomeController : BaseController
    {
        // Logging Utilities
        protected NLogLogger nlog = new NLogLogger();
        protected Log4NetLogger log4net = new Log4NetLogger();

        public ActionResult Index()
        {
            //Elmah.ErrorLogDataSourceAdapter adpt = new Elmah.ErrorLogDataSourceAdapter();
            //int i = adpt.GetErrorCount();

            //IEnumerable list = activityRepository.GetAll();

            //NLogLogger logger = new NLogLogger();
            //logger.Info("We're on the Index page for Activities");
            //Logic.BaseDataClass.CreateMovie();
            try
            {
                //throw new Exception("A test exception");
            }
            catch (Exception ex)
            {
               // logger.Error("An error has occurred", ex);
            }

            //log4net.Debug("Here is a debug log.");
            //log4net.Info("... and an Info log.");
            //log4net.Warn("... and a warning.");
            //log4net.Error("... and an error.");
            //log4net.Fatal("... and a fatal error.");

            //nlog.Debug("Here is a debug log.");
            //nlog.Info("... and an Info log.");
            //nlog.Warn("... and a warning.");
            //nlog.Error("... and an error.");
            //nlog.Fatal("... and a fatal error.");


            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult ThemeView()
        {
            return View();
        }



        public ActionResult ChatterBox()
        {
            return View();
        }

    }
}
