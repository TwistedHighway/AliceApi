using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliceApi.Repository.Models;
using AliceApi.Repository.Repositories;
using AliceApi.Repository.Repositories.Membership;
using AliceApi.Repository.Repositories.Movies;

namespace AliceApi.Repository
{
    public class UnitOfWork : IDisposable
    {
        // TODO: Consider refactoring this repository and leverage something like Ninject 
        //private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); // unused?

        #region Members
        //http://www.asp.net/mvc/tutorials/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

        private readonly BaseEntities _baseEntities = new BaseEntities();
        private readonly LoggingEntities _loggingEntities = new LoggingEntities();
        private LoggedInUserContext _loggedInUserContext;

        private MovieRepository _movieRepository;
        private MovieGenreRepository _movieGenreRepository;
        private MovieMpaaRepository _movieMpaaRepository;

        private MemberProfileRepository _memberProfileRepository;
        private MemberRepository _memberRepository;
        

        /// <summary>
        ///  HOUSEKEEPING
        /// </summary>
        private bool _disposed;


        #endregion

        #region Constructor/Destructor

        public UnitOfWork(LoggedInUserContext loggedInUserContext)
        {
            _loggedInUserContext = loggedInUserContext;
        }

        public UnitOfWork() { }

        // because we use IDisposable 
        //http://stackoverflow.com/questions/4898733/when-should-i-create-a-destructor
        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public LoggedInUserContext UserContext
        {
            get
            {
                //if (_loggedInUserContext != null) return _loggedInUserContext;
                //if (HttpContext.Current.Session["UserContext"] != null)
                //{
                //    return (LoggedInUserContext) HttpContext.Current.Session["UserContext"];
                //}
                return _loggedInUserContext;
            }
            set { _loggedInUserContext = value; }
        }

        public BaseEntities BaseEntities
        {
            get { return _baseEntities; }
        }




        #region Movies


        public MovieRepository MovieRepository
        {
            get { return _movieRepository ?? (_movieRepository = new MovieRepository(_baseEntities, UserContext)); }
        }

        public MovieGenreRepository MovieGenreRepository
        {
            get { return _movieGenreRepository ?? (_movieGenreRepository = new MovieGenreRepository(_baseEntities, UserContext)); }
        }

        public MovieMpaaRepository MovieMpaaRepository
        {
            get { return _movieMpaaRepository ?? (_movieMpaaRepository = new MovieMpaaRepository(_baseEntities, UserContext)); }
        }


    #endregion
        


        #region Membership

        public MemberProfileRepository MemberProfileRepository
        {
            get { return _memberProfileRepository ?? (_memberProfileRepository = new MemberProfileRepository(_baseEntities, UserContext)); }
        }

        public MemberRepository MemberRepository
        {
            get { return _memberRepository ?? (_memberRepository = new MemberRepository(_baseEntities, UserContext)); }
        }

#endregion
        

        public void Save()
        {
            if (_loggedInUserContext == null) throw new ApplicationException("Unknown User context!");
            //TODO: Reinstate as central auditing is implemented
            //foreach (
            //    var ent in
            //        this._SalesApp1Entities.ChangeTracker.Entries()
            //            .Where(
            //                p =>
            //                    p.State == (EntityState) System.Data.EntityState.Added || p.State == (EntityState) System.Data.EntityState.Deleted || p.State == (EntityState) System.Data.EntityState.Modified))
            //{
            //    //TODO: Implement addition of audit row: http://stackoverflow.com/questions/20961489/how-to-create-an-audit-trail-with-entity-framework-5-and-mvc-4
            //}

            _baseEntities.SaveChanges();
            _loggingEntities.SaveChanges();
        }

        #endregion

        #region Entities Direct

        public LoggingEntities LoggingEntities
        {
            get { return _loggingEntities; }
        }

        #endregion
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _baseEntities.Dispose();
                    _loggingEntities.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }





    }
}
