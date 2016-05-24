using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories.Movies
{
   

    public class MovieGenreRepository : GenericRepository<Models.MovieGenre>, Interfaces.IMovieRepository
    {
        public MovieGenreRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        {
        }
    }


}
