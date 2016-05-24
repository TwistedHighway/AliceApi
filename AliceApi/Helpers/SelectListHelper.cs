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

        public class SelectViewModel
        {
            public string GenreId { get; set; }
            public IEnumerable<SelectListItem> List { get; set; }
        }
    }
}
