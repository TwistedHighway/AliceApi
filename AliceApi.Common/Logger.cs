using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliceApi.Common
{

   public class Logger
    {



        ////Response.Write(log4net.LogicalThreadContext.Properties.ToString());
        ////string msg = "Foo";
        //log.Error(ex);

        public void LogError(string theMessage, string theException)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("ErrorLogger");
            //log4net.Config.XmlConfigurator.Configure(); // best
            //log.Error(theMessage + " "  + theException);
            //log.Info
        }

        //public void LogError(string theErrorMessage)
        //{
        //    log4net.ILog log = log4net.LogManager.GetLogger("ErrorLogger");
        //    log4net.Config.XmlConfigurator.Configure(); // best
        //    log.Error(theErrorMessage);
        //    //log.Info
        //}

        //public void LogInfo(string theInfoToLog)
        //{
        //    log4net.ILog log = log4net.LogManager.GetLogger("ErrorLogger");
        //    log4net.Config.XmlConfigurator.Configure(); // best
        //    log.Error(theInfoToLog);
        //}

        public void LogInfo(string theInfoToLog, string theException)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("ErrorLogger");
            //log4net.Config.XmlConfigurator.Configure(); // best
            //log.Info(theInfoToLog + " " + theException);
        }


    }




}
