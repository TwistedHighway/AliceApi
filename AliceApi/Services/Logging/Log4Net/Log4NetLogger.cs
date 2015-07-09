using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net;

namespace AliceApi.Services.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        //http://logging.apache.org/log4net/release/manual/configuration.html
        //Example usage:
        //------
        // Configure log4net using the .config file
        //[assembly: log4net.Config.XmlConfigurator(Watch=true)]
        // This will cause log4net to look for a configuration file
        // called TestApp.exe.config in the application base
        // directory (i.e. the directory containing TestApp.exe)
        // The config file will be watched for changes.
        //-----              
        // Configure log4net using the .log4net file
        //[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension="log4net",Watch=true)]
        // This will cause log4net to look for a configuration file
        // called TestApp.exe.log4net in the application base
        // directory (i.e. the directory containing TestApp.exe)
        // The config file will be watched for changes.
                            
        // NOTE: 
        // Using attributes can be a clearer method for defining where the application's 
        // configuration will be loaded from. However it is worth noting that attributes are purely passive. 
        // They are information only. Therefore if you use configuration attributes 
        // you must invoke log4net to allow it to read the attributes. 
        // A simple call to LogManager.GetLogger will cause the attributes on the calling 
        // assembly to be read and processed. Therefore it is imperative to make a logging 
        // call as early as possible during the application start-up, and certainly before 
        // any external assemblies have been loaded and invoked.

        // IMPORTANT
        // The only way to configure an application using the System.Configuration APIs is 
        // to call the log4net.Config.XmlConfigurator.Configure() method 
        // or the log4net.Config.XmlConfigurator.Configure(ILoggerRepository) method. 

        private ILog _logger;

        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure(); // ?? should we need this? 
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("Web.config"));
            _logger = LogManager.GetLogger(this.GetType());
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }
    }
}

