using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AliceApi.Repository.Models;

//using LinqKit;
using AliceApi.Repository.Interfaces;


namespace AliceApi.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <remarks>Good article as basis to possibly extend this generic repository:http://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet</remarks>
    public class GenericRepository<TEntity> where TEntity : class
    {
        //private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool _disposed;
        private readonly LoggedInUserContext _userContext;
        private readonly Collection<AuditLog> _auditTrailList = new Collection<AuditLog>();
        private readonly BaseEntities _baseContext;        
        private readonly LoggingEntities _loggingContext;
        private readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(BaseEntities baseContext, LoggedInUserContext userContext = null)
        {
             _userContext = userContext;
            _baseContext = baseContext;
            //_loggingContext = loggingContext;
            _dbSet = baseContext.Set<TEntity>();

        }

        #region Get
        public IEnumerable<TEntity> Get()
        {
            return GetQuery(null, String.Empty);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return GetQuery(filter, String.Empty);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return GetQuery(filter, includeProperties);
        }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public virtual TEntity GetById(object id)
        {
            if (!(id is Guid)) return _dbSet.Find(id);

            var guid = (Guid)id;
            return _dbSet.Find(guid);
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        #endregion

        public virtual void Insert(TEntity entity)
        {
            _baseContext.Entry(entity).State = EntityState.Added;

            if (entity is IAuditable)
            {
                var auditEntity = (IAuditable)entity;
                auditEntity.CreateDate = DateTime.Now;
                if (_userContext != null)
                {
                    auditEntity.CreatedBy = _userContext.UserName;
                    auditEntity.CreateDate = DateTime.Now;
                    auditEntity.UpdatedDate = DateTime.Now;
                    auditEntity.UpdatedBy = _userContext.UserName;
                }
                else
                {
                    auditEntity.CreatedBy = "No User Specified";
                    auditEntity.CreateDate = DateTime.Now;
                    auditEntity.UpdatedDate = DateTime.Now;
                    auditEntity.UpdatedBy = "No User Specified";
                }

                _dbSet.Add(entity);
            }
            //else if (entity is ICreateAuditable)
            //{
            //    var auditEntity = (ICreateAuditable)entity;
            //    auditEntity.CreateDate = DateTime.Now;
            //    if (_userContext != null)
            //    {
            //        auditEntity.CreatedBy = _userContext.UserId;
            //    }

            //    _dbSet.Add(entity);
            //}
            else
            {
                _dbSet.Add(entity);
            }

        }

        public virtual void Delete(Guid id)
        {

            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete is IsDeleted)
            {
                var softDelete = (IsDeleted)entityToDelete;
                softDelete.IsDeleted = true;
                _loggingContext.Entry(softDelete).State = EntityState.Modified;
            }
            else
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete is IsDeleted)
            {
                var softDelete = (IsDeleted)entityToDelete;
                softDelete.IsDeleted = true;
                //_loggingContext.Entry(softDelete).State = EntityState.Modified;
            }
            else
            {
                Delete(entityToDelete);
            }
        }

        protected virtual void Delete(TEntity entityToDelete)
        {
            if (_baseContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            if (entityToDelete is IsDeleted)
            {
                var softDelete = (IsDeleted)entityToDelete;
                softDelete.IsDeleted = true;
                //_loggingContext.Entry(softDelete).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Remove(entityToDelete);
            }

        }

        public virtual void Update(TEntity entityToUpdate)
        {
            

            if (_userContext != null && entityToUpdate is IAuditable)
            {
                var auditEntity = (IAuditable)entityToUpdate;
                auditEntity.CreateDate = auditEntity.CreateDate;
                auditEntity.CreatedBy = auditEntity.CreatedBy;
                auditEntity.UpdatedDate = DateTime.Now;
                
                if (_userContext != null)
                {
                    //auditEntity.UpdatedBy = _userContext.UserId; // 
                    auditEntity.UpdatedBy = _userContext.UserName;
                }
                //_loggingContext.Entry(auditEntity).State = EntityState.Modified;
            }
            else
            {
                //_loggingContext.Entry(entityToUpdate).State = EntityState.Modified;
            }
        }

        public void Save()
        {
            try
            {
                _loggingContext.SaveChanges();
                _baseContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Logger.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //foreach (var ve in eve.ValidationErrors)
                    //{
                    //    Logger.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    //}
                }
                throw;
            }
        }


        private void DoAudit(DbEntityEntry dbEntry)
        {
            var userName = _userContext == null ? "No Domain" : _userContext.UserName;

            var manager = ((IObjectContextAdapter)_loggingContext).ObjectContext.ObjectStateManager;
            ObjectStateEntry entry = manager.GetObjectStateEntry(dbEntry.Entity);

            if (dbEntry.State == EntityState.Added)
            {
                //var id = dbEntry.CurrentValues.GetValue<object>(keyName).ToString();
                var id = entry.EntityKey;

                // For Inserts, just add the whole record
                foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    if (propertyName == "ActivityEntryId")
                    {
                        var actid = dbEntry.CurrentValues[propertyName];
                    }
                    _auditTrailList.Add(new AuditLog()
                    {
                        AuditLogId = Guid.NewGuid(),
                        UserName = userName,
                        EventDateTime = DateTime.Now,
                        EventType = "Added",
                        TableName = entry.EntitySet.ToString(),
                        //dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),// keyName won't work because we're not CodeFirst
                        //RecordId = Guid.Parse(entry.EntityKey.EntityKeyValues[0].Value.ToString()), // CANNOT GET PRIMARY KEY at this point need to run SaveChanges First
                        ColumnName = propertyName,
                        NewValue = dbEntry.CurrentValues[propertyName] == null ? null : dbEntry.CurrentValues[propertyName].ToString()
                    });
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                //stateEntryEntity.OriginalValues.ToObject();
                _auditTrailList.Add(new AuditLog()
                {
                    AuditLogId = Guid.NewGuid(),
                    UserName = userName,
                    EventDateTime = DateTime.Now,
                    EventType = "Deleted",
                    TableName = entry.EntitySet.ToString(),
                    RecordId = Guid.Parse(entry.EntityKey.EntityKeyValues[0].Value.ToString()),
                    ColumnName = "*ALL",
                    //NewValue = (stateEntryEntity.OriginalValues.ToObject() is IDescribableEntity) ? (stateEntryEntity.OriginalValues.ToObject() as IDescribableEntity).Describe() : stateEntryEntity.OriginalValues.ToObject().ToString()
                });
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    // For updates, we only want to capture the columns that actually changed
                    if (!Equals(dbEntry.OriginalValues[propertyName], dbEntry.CurrentValues[propertyName]))
                    {
                        _auditTrailList.Add(new AuditLog()
                        {
                            AuditLogId = Guid.NewGuid(),
                            UserName = userName,
                            EventDateTime = DateTime.Now,
                            EventType = "Modified",
                            TableName = entry.EntitySet.ToString(),
                            RecordId = Guid.Parse(entry.EntityKey.EntityKeyValues[0].Value.ToString()),
                            ColumnName = propertyName,
                            OriginalValue = dbEntry.OriginalValues[propertyName] == null ? null : dbEntry.OriginalValues[propertyName].ToString(),
                            NewValue = dbEntry.CurrentValues[propertyName] == null ? null : dbEntry.CurrentValues[propertyName].ToString()
                        });
                    }
                }
            }

            // add the rows to the database
            if (_auditTrailList.Count > 0)
            {
                foreach (var audit in _auditTrailList)
                {
                    _loggingContext.AuditLogs.Add(audit);
                }
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _baseContext.Dispose();
                    _loggingContext.Dispose();
                }
            }
            _disposed = true;
        }


    }

}
