using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AliceApi.Logic.Movies;
using AliceApi.ViewModels.Movie;

namespace AliceApi.Controllers
{
    public partial class MovieController : Controller
    {

        private MovieLogic _logic;

        public MovieController()
        {
            _logic = new MovieLogic();
        }
        //
        // GET: /Movie/
        public virtual ActionResult Index()
        {
            var model = new MovieViewModel.Movie();
            var logic = new Logic.Movies.MovieLogic();
            model.Movies = logic.GetAll();
            return View(model);
        }

        //
        // GET: /Movie/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Movie/Edit/5
        public virtual ActionResult Edit(int id)
        {
            if (id == 0)
                return View(new MovieViewModel.Movie());

            var data = _logic.GetOne(id);
            return View(data);
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
