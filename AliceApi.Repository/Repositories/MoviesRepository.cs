﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceApi.Repository.Models;

namespace AliceApi.Repository.Repositories
{
    public class MovieRepository : GenericRepository<Models.Movie>, Interfaces.IMovieRepository
    {
        public MovieRepository(BaseEntities baseContext, LoggedInUserContext userContext)
            : base(baseContext, userContext)
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
}