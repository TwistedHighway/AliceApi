using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AliceApi.Areas.Products.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/Products/

        public ActionResult Index()
        {
            return View();
        }

    }
}
