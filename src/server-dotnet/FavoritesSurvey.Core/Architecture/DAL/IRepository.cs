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
    public interface IRepository<T> where T : class, IModel
    {
        IQueryable<T> AsQuery(Func<DbSet<T>, IQueryable<T>> includes = null);
        T Find(Expression<Func<T, bool>> criteria);
        TNew Find<TNew>(Expression<Func<T, bool>> criteria, Func<T, TNew> projection);
        T FindById(int id);
        TNew FindById<TNew>(int id, Func<T, TNew> projection);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria);
        IEnumerable<TNew> FindAll<TNew>(Expression<Func<T, bool>> criteria, Func<T, TNew> projection);
        IEnumerable<T> GetAll();
        IEnumerable<TNew> GetAll<TNew>(Func<T, TNew> projection);
        void Insert(T item, int contextUserId);
        void Update(T item, int contextUserId, Action<T> changes);
        void Remove(T item, int contextUserId);
        void RemoveWithRowVersion<TRowVersion>(TRowVersion item, int contextUserId, string rowVersion) where TRowVersion : class, IModel, IModelRowVersion;
        void UpdateWithPull(Expression<Func<T, bool>> criteria, int contextUserId, Action<T> changes, Func<DbSet<T>, IQueryable<T>> includes = null);
        void Upsert(Expression<Func<T, bool>> criteria, int contextUserId, T item, Action<T> changes, Func<DbSet<T>, IQueryable<T>> includes = null);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindActiveAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindActiveByIdAsync(int id);
        Task<IEnumerable<T>> FindAllActiveAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllActiveAsync();
    }
}
