using ApplicationBoot.Annotations;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    [Service(typeof(IRepository), LifeTimeScope.Application)]
    public class Repository : IRepository
    {
        private readonly ChatEntities contextContainer;

        public Repository()
        {
            SqlProviderServices instance = SqlProviderServices.Instance;

            this.contextContainer = new ChatEntities();
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            DbSet<T> dbSet = this.contextContainer.Set<T>();
            return dbSet;
        }

        public void Add<T>(T entity) where T : class
        {
            if (this.contextContainer.Entry(entity).State == EntityState.Detached)
                this.contextContainer.Entry(entity).State = EntityState.Added;
        }

        public void Update<T>(T entity) where T : class
        {
            this.contextContainer.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            if (entity != null)
            {
                this.contextContainer.Entry(entity).State = EntityState.Deleted;
            }
        }

        public void SaveChanges()
        {
            this.contextContainer.SaveChanges();
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return this.GetEntities<T>().Where(expression);
        }

        public T FindSingle<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return this.Find(expression).FirstOrDefault();
        }

        public void Dispose()
        {
            this.contextContainer.Dispose();
        }
    }
}
