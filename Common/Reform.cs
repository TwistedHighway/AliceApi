using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public class Reform
    {

        /// <summary>
        /// Helper function for format text into proper spacing and case 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SeperateCamelCase(string value)
        {
            string fixit = string.Empty;
            fixit = Regex.Replace(value, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1");// magic!

            // at least one exception... 
            fixit = fixit.Replace("And", "and");

            return fixit;
        }
    }
}
