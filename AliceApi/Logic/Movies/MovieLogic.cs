using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AliceApi.ViewModels.Movie;
using AliceApi.Repository;
using AliceApi.Repository.Models;

namespace AliceApi.Logic.Movies
{
    public class MovieLogic
    {


        //public MovieViewModel movie = new MovieViewModel();

        public void Insert(MovieViewModel.Movie model)
        {
            var uow = new UnitOfWork();
            using (uow)
            {
                var movie = new Repository.Models.Movie()
                {
                    
                    MovieTitle = model.MovieTitle,
                    
                };
                uow.MovieRepository.Insert(movie);
            }
        }


        public void Update(MovieViewModel.Movie model)
        {
            var uow = new UnitOfWork();
            using (uow)
            {
                var movie = new Repository.Models.Movie()
                {
                    MovieId = model.MovieId,
                    MovieTitle = model.MovieTitle,
                };
                uow.MovieRepository.Update(movie);
            }
        }


        public void Delete(int id)
        {
            var uow = new UnitOfWork();
            uow.MovieRepository.Delete(id);
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
                    ReleaseDate = item.ReleaseDate ?? DateTime.MinValue,
                    MpaaRated = item.MovieMPAA.MPAAName,
                    Genre = GetGenreList(item.Genre ?? 0)
                });
                
            }
            return movieList;
        }

        private string GetGenreList(int gid)
        {
            if (gid == 0)
               return "";
            
            var myString = new StringBuilder();
            var uow = new UnitOfWork();
            var data= uow.MovieGenreRepository.Get().Where(w => (w.GenreId & gid) > 0);
            foreach (var row in data)
            {
                myString.Append(row.GenreName);
                myString.Append(", ");
            }
            return myString.ToString().Trim().Trim(',');
        }

        public IList<MovieGenre> Genres
        {
            get
            {
                var uow = new UnitOfWork();
                return uow.MovieGenreRepository.Get().ToList();
            }
        }

        public MovieViewModel.Movie GetOne(int id)
        {
            var uow = new UnitOfWork();
            var movie = uow.MovieRepository.GetById(id);
            var model = new MovieViewModel.Movie()
            {
                MovieId = movie.MovieId,
                MovieTitle = movie.MovieTitle,
                ReleaseDate = movie.ReleaseDate ?? DateTime.MinValue,
                Genre = movie.Genre.ToString(),
                Genres = Genres
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






    }
}