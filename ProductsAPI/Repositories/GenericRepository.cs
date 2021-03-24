using Microsoft.EntityFrameworkCore;
using ProductsAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext Context { get; set; }
        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TEntity>();
                return _dbSet;
            }
        }
        private DbSet<TEntity> _dbSet;

        public virtual IList<TEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual object Insert(TEntity entity, bool saveChanges = false)
        {
            var rtn = this.DbSet.Add(entity);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
            return rtn;
        }

        public virtual void Delete(object id, bool saveChanges = false)
        {
            var item = GetById(id);
            this.DbSet.Remove(item);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity, bool saveChanges = false)
        {
            var entry = Context.Entry(entity);
            this.DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Commit()
        {
            Context.SaveChanges();
        }
        

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
