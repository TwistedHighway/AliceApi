using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliceApi.Repository;
namespace AliceApi.Logic
{
    public class BaseDataClass
    {

        public static void CreateMovie()
        {
            var uc = new LoggedInUserContext { UserId = new Guid(), UserName = "Bruce" };
            var uow = new AliceApi.Repository.UnitOfWork(uc);
            using (uow)
            {
                var movie = new AliceApi.Repository.Models.Movie { 
                    MovieTitle = "Star Wars"
                };
                uow.MovieRepository.Insert(movie);
                uow.Save();
                var foo = uow.MovieRepository.Get();
            }



        }


    }
}