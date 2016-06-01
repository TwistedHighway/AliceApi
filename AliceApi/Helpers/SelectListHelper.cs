using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AliceApi.Helpers
{
    class SelectListHelper 
    {
        //http://www.asp.net/mvc/overview/older-versions-1/views/creating-custom-html-helpers-cs
        public class SelectViewModel
        {
            public string GenreId { get; set; }
            public IEnumerable<SelectListItem> List { get; set; }
        }


//        public static MvcHtmlString ActionLink(
//    this HtmlHelper htmlHelper,
//    string linkText,
//    string actionName,
//    Object routeValues,
//    Object htmlAttributes
//)
    }
}
