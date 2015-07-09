using System;
using System.Configuration;

namespace AliceApi
{
    public class Configuration
    {
        // application name (what should be displayed to user)
        public static string AppName
        {
            get
            {
                string name = "AliceApi"; // put application name here
#if DEV || DEBUG
                name += "AliceApi - DEV";
#endif
                return name;
            }
        }

        // Database connection strings
        public static string LoggingConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["LoggingEntitiesConnection"].ConnectionString; }
        }

        //public static string CoreConnectionString
        //{
        //    get { return ConfigurationManager.ConnectionStrings["x_Core"].ConnectionString; }
        //}

        // App settings
        //public static Guid RootObjectUid
        //{
        //    get { return new Guid(AppSettings("RootObjectUid")); }
        //}

        private static string AppSettings(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            return ConfigurationManager.AppSettings[key];
        }
    }
}