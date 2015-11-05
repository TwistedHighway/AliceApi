using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AliceApi.Controllers
{
    public partial class MovieController : Controller
    {
        //
        // GET: /Movie/

        public virtual ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Movie/Details/5

        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Movie/Create

        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movie/Create

        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Movie/Edit/5

        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Movie/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                //Alice1.Helpers.
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Movie/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Movie/Delete/5

        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
