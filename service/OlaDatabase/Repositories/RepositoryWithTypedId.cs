using NHibernate;
using OlaDatabase.RepositoryInterfaces;
using OlaDatabase.Session;
using System.Collections.Generic;
using System.Linq;

namespace OlaDatabase.Repositories
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class
    {
        public IEnumerable<T> Repository
        {
            get
            {
                return Session.Query<T>();
            }
        }

        protected virtual ISession Session
        {
            get
            {
                return SessionFactoryHelper.GetSession();
            }
        }

        public virtual T Get(TId id)
        {
            return Session.Get<T>(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public virtual T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        /// <summary>
        /// This deletes the object and commits the deletion immediately.  We don't want to delay deletion
        /// until a transaction commits, as it may throw a foreign key constraint exception which we could
        /// likely handle and inform the user about.  Accordingly, this tries to delete right away; if there
        /// is a foreign key constraint preventing the deletion, an exception will be thrown.
        /// </summary>
        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
            Session.Flush();
        }
    }
}
