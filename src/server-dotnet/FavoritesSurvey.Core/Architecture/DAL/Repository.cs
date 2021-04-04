using FavoritesSurvey.Core.Architecture.BLL;
using FavoritesSurvey.Core.Architecture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.Core.Architecture.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : class, IModel
    {
        protected IContext _context;

        public Repository(IContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<T> AttachIncludes(DbSet<T> dbSet)
        {
            return dbSet;
        }

        public virtual IQueryable<T> AsQuery(Func<DbSet<T>, IQueryable<T>> includes = null)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);

            if (includes != null)
                dbIncludes = includes(dbIncludes as DbSet<T>);

            return dbIncludes;
        }

        public virtual T Find(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.FirstOrDefault(criteria);
        }

        public virtual TNew Find<TNew>(Expression<Func<T, bool>> criteria, Func<T, TNew> projection)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(criteria).Select(projection).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(criteria).ToList();
        }

        public virtual IEnumerable<TNew> FindAll<TNew>(Expression<Func<T, bool>> criteria, Func<T, TNew> projection)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(criteria).Select(projection).ToList();
        }

        public virtual T FindById(int id)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.SingleOrDefault(i => i.Id == id);
        }

        public virtual TNew FindById<TNew>(int id, Func<T, TNew> projection)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(i => i.Id == id).Select(projection).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.ToList();
        }

        public virtual IEnumerable<TNew> GetAll<TNew>(Func<T, TNew> projection)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Select(projection).ToList();
        }

        public virtual void Insert(T item, int contextUserId)
        {
            var internalContext = _context.GetContext();
            internalContext.Add<T>(item, contextUserId);
        }

        public virtual void Remove(T item, int contextUserId)
        {
            Update(item, contextUserId, i => i.IsActive = false);
        }

        public virtual void RemoveWithRowVersion<TRowVersion>(TRowVersion item, int contextUserId, string rowVersion) where TRowVersion : class, IModel, IModelRowVersion
        {
            Update(item as T, contextUserId, i =>
            {
                i.IsActive = false;
                var cast = i as IModelRowVersion;
                if (cast != null)
                    cast.RowVersion.CopyRowVersion(rowVersion);
            });
        }

        public virtual void Update(T item, int contextUserId, Action<T> changes)
        {
            var internalContext = _context.GetContext();
            internalContext.Modify<T>(item, contextUserId, changes);
        }

        public void UpdateWithPull(Expression<Func<T, bool>> criteria, int contextUserId, Action<T> changes, Func<DbSet<T>, IQueryable<T>> includes = null)
        {
            var internalContext = _context.GetContext();
            internalContext.ModifyPull<T>(criteria, contextUserId, changes, includes);
        }

        public void Upsert(Expression<Func<T, bool>> criteria, int contextUserId, T item, Action<T> changes, Func<DbSet<T>, IQueryable<T>> includes = null)
        {
            var internalContext = _context.GetContext();
            var exists = internalContext.ModifyPull<T>(criteria, contextUserId, changes, includes);
            if (!exists)
                Insert(item, contextUserId);
        }

        public virtual Task<T> FindAsync(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.FirstOrDefaultAsync(criteria);
        }

        public virtual Task<T> FindByIdAsync(int id)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async virtual Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return await dbIncludes.Where(criteria).ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return await dbIncludes.ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAllActiveAsync()
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return await dbIncludes.Where(i => i.IsActive).ToListAsync();
        }

        public virtual Task<T> FindActiveAsync(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(i => i.IsActive).FirstOrDefaultAsync(criteria);
        }

        public virtual Task<T> FindActiveByIdAsync(int id)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return dbIncludes.Where(i => i.IsActive).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async virtual Task<IEnumerable<T>> FindAllActiveAsync(Expression<Func<T, bool>> criteria)
        {
            var db = _context.Get<T>();
            var dbIncludes = AttachIncludes(db);
            return await dbIncludes.Where(i => i.IsActive).Where(criteria).ToListAsync();
        }
    }
}
