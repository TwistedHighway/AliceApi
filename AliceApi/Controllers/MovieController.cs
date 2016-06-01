using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ViewResult> Index()
        {
            var test = new InternetVideoArchive();
            await test.FetchData();

            var model = new MovieViewModel.Movie();
            var logic = new Logic.Movies.MovieLogic();
            model.Movies = logic.GetAll();
            return View(model);
        }

        //
        // GET: /Movie/Details/5
        public virtual ActionResult Details(int id)
        {
            var model = _logic.GetOne(id);
            return View(model);
        }

        //
        // GET: /Movie/Edit/5
        public virtual ActionResult Edit(int id)
        {
            if (id == 0)
                return View(new MovieViewModel.Movie()
                {
                    MovieId = 0,
                    Genres = _logic.Genres
                });

            var data = _logic.GetOne(id);
            return View(data);
        }

        //
        // POST: /Movie/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(int id, MovieViewModel.Movie model, FormCollection collection)
        {
            try
            {
                var logic = new Logic.Movies.MovieLogic();
                var movie = new MovieViewModel.Movie();

                // Set up the Genre BitShift Field
                var selectedGenres = Request.Form["Genres"];
                var genreIdList = selectedGenres.Split(',');

                if (genreIdList.Length == 0)
                {model.Genres = null;}
                else
                {
                    var genre = genreIdList.Sum(int.Parse);
                    model.Genre = genre;
                }

                if (id == 0)
                {
                    // Insert
                    model.MpaaRatingId = 1;
                    movie = model;
                    logic.Insert(movie);
                }
                else
                {
                    // Update
                    var current = logic.GetOne(id);
                    current.MovieTitle = model.MovieTitle;
                    current.Genre = model.Genre;
                    current.ReleaseDate = model.ReleaseDate;
                    current.MpaaRatingId = 1;
                    logic.Update(movie);
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.ErrorId = 1;
                model.Message = ex.Message;
                return View(model);
            }
        }

        //
        // POST: /Movie/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            // put the result in a variable because we may want to do something with it. 
            string result =_logic.Delete(int.Parse(id));
            return Json(result, JsonRequestBehavior.AllowGet);
           
        }
    }
}
