#region

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dal
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> GetEntities<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void SaveChanges();
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class;
        T FindSingle<T>(Expression<Func<T, bool>> expression) where T : class;
    }
}