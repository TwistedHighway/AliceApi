using System;

namespace AliceApi.Helpers
{
    public class LabelHelper
    {
        //http://www.asp.net/mvc/overview/older-versions-1/views/creating-custom-html-helpers-cs
        public static string Label(string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }
    }
}
