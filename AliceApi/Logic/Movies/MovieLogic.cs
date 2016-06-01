using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AliceApi.ViewModels.Movie;
using AliceApi.Repository;
using AliceApi.Repository.Models;
using Microsoft.AspNet.Identity;

namespace AliceApi.Logic.Movies
{
    public class MovieLogic
    {

        //public MovieViewModel movie = new MovieViewModel();

        public void Insert(MovieViewModel.Movie model)
        {
            //var userContext = new LoggedInUserContext()
            //{
            //    UserId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId()),
            //    UserName = HttpContext.Current.User.Identity.Name
            //};

            var userContext = new LoggedInUserContext()
            {
                UserId = Guid.Empty,
                UserName = "system"
            };
            
            var uow = new UnitOfWork(userContext);
                using (uow)
                {
                    var movie = new Repository.Models.Movie()
                    {
                        MovieTitle = model.MovieTitle,
                        ReleaseDate = model.ReleaseDate,
                        Genre = model.Genre,
                        MPAAID = model.MpaaRatingId
                    };
                    uow.MovieRepository.Insert(movie);
                    uow.Save();
                }
           
        }


        public void Update(MovieViewModel.Movie model)
        {
            var userContext = new LoggedInUserContext()
            {
                UserId = Guid.Empty,
                UserName = "system"
            };
            var uow = new UnitOfWork(userContext);
            using (uow)
            {
                var movie = new Repository.Models.Movie()
                {
                    MovieId = model.MovieId,
                    MovieTitle = model.MovieTitle,
                    ReleaseDate = model.ReleaseDate,
                    Genre = model.Genre
                };
                uow.MovieRepository.Update(movie);
                uow.MovieRepository.Save();
            }
        }


        public string Delete(int id)
        {
            var userContext = new LoggedInUserContext()
            {
                UserId = Guid.Empty,
                UserName = "system"
            };
            try
            {
                var uow = new UnitOfWork(userContext);
                uow.MovieRepository.Delete(id);
                uow.Save();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IList<MovieViewModel.Movie> GetAll()
        {
            var movieList = new List<MovieViewModel.Movie>();
            var uow = new UnitOfWork();

            var data = uow.MovieRepository.Get().ToList();

            //var other = from r in data where (r.Genre & 8) == 8 select r;

            //var gg = data.Where(w => (w.Genre & g.FirstOrDefault(r => r.GenreId == 2).GenreId) > 0);

            foreach (var item in data)
            {
                //var bitdata = ((item.Genre & g.FirstOrDefault().GenreId) > 0);
                
                movieList.Add(new MovieViewModel.Movie()
                {
                    MovieId = item.MovieId,
                    MovieTitle = item.MovieTitle,
                    ReleaseDate = item.ReleaseDate,
                    MpaaRated = item.MovieMPAA.MPAAName,
                    GenreList = GetGenreList(item.Genre ?? 0),
                    Genre = item.Genre ?? 0
                });
                
            }
            return movieList;
        }
        

        public MovieViewModel.Movie GetOne(int id)
        {
            var uow = new UnitOfWork();
            var movie = uow.MovieRepository.GetById(id);
            var model = new MovieViewModel.Movie()
            {
                MovieId = movie.MovieId,
                MovieTitle = movie.MovieTitle,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre ?? 0,
                Genres = Genres,
                MpaaRatingId = movie.MPAAID,
            };
            return model;
        }
        

        public List<MovieViewModel.Movie> GetByParam(string param)
        {
            var myList = new List<MovieViewModel.Movie>();

            var uow = new UnitOfWork();
            foreach (var mov in uow.MovieRepository.Get())
            {
                myList.Add(new MovieViewModel.Movie()
                {
                    MovieTitle = mov.MovieTitle
                });
            }
            return myList;
        }




        /// <summary>
        /// Genre List - Comma separated list of genres
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        private string GetGenreList(int gid)
        {
            if (gid == 0)
                return "";

            var myString = new StringBuilder();
            var uow = new UnitOfWork();
            var data = uow.MovieGenreRepository.Get().Where(w => (w.GenreId & gid) > 0);
            foreach (var row in data)
            {
                myString.Append(row.GenreName);
                myString.Append(", ");
            }
            return myString.ToString().Trim().Trim(',');
        }

        /// <summary>
        /// All Genres from the database (could be moved to a Model) -- this is why we need an anonymous UOW
        /// </summary>
        public IList<MovieGenre> Genres
        {
            get 
            {
                var uow = new UnitOfWork();
                return uow.MovieGenreRepository.Get().ToList();
            }
        }




    }
}