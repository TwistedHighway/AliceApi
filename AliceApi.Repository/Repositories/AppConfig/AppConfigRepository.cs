using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories.AppConfig
{
    public class AppConfigRepository : GenericRepository<Models.AppConfig>, Interfaces.IMovieRepository
    {
        public AppConfigRepository(BaseEntities baseContext, LoggedInUserContext userContext) : base(baseContext, userContext)
        { 
        }
  
    }



}
