using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories.Movies
{
    public class MovieRepository : GenericRepository<Models.Movie>, Interfaces.IMovieRepository
    {
        public MovieRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        { 
        }
        //public class ConstructionChargeRepository : GenericRepository<ConstructionCharge>, IConstructionChargeRepository
        //{
        //    #region Constructor
        //    public ConstructionChargeRepository(SyringaWebEntities context, LoggedInUserContext loggedInUser)
        //        : base(context, loggedInUser)
        //    { }
        //    #endregion
        //}
    }

    public class MovieGenreRepository : GenericRepository<Models.MovieGenre>, Interfaces.IMovieRepository
    {
        public MovieGenreRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        {
        }
    }

    public class MovieMpaaRepository : GenericRepository<Models.MovieMPAA>, Interfaces.IMovieRepository
    {
        public MovieMpaaRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        {
        }
    }

}
