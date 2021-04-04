using FavoritesSurvey.Core.Architecture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.DAL
{
    public static class ContextExtensions
    {
        public static bool ModifyPull<T>(this DbContext context, Expression<Func<T, bool>> criteria, int contextUserId, Action<T> changes = null, Func<DbSet<T>, IQueryable<T>> includes = null) where T : class, IModel
        {
            var oldModel = includes != null ? includes(context.Set<T>()).FirstOrDefault(criteria) : context.Set<T>().FirstOrDefault(criteria);

            var exists = oldModel != null;
            if (exists)
            {
                changes?.Invoke(oldModel);

                if (context.Entry(oldModel).State == EntityState.Unchanged)
                    return exists;

                oldModel.ModifiedBy = contextUserId;
                oldModel.DateModified = DateTime.Now;
                context.Entry(oldModel).CurrentValues.SetValues(oldModel);
            }

            return exists;
        }

        public static void Modify<T>(this DbContext context, T item, int contextUserId, Action<T> changes = null) where T : class, IModel
        {
            if (item != null)
            {
                context.Set<T>().Attach(item);

                changes?.Invoke(item);
                item.ModifiedBy = contextUserId;
                item.DateModified = DateTime.Now;

                context.Entry(item).Property(i => i.DateCreated).IsModified = false;
                context.Entry(item).Property(i => i.CreatedBy).IsModified = false;
            }
        }

        public static void Add<T>(this DbContext context, T model, int contextUserId) where T : class, IModel
        {
            if (model != null)
            {
                model.CreatedBy = contextUserId;
                model.ModifiedBy = 0;
                model.DateCreated = DateTime.Now;
                model.DateModified = DateTime.Now;
                model.IsActive = true;
                context.Set<T>().Add(model);
            }
        }

        public static IQueryable<T> Paginate<T>(this IContext context, IOrderedQueryable<T> initialQuery, PaginationParameter pagination)
        {
            var page = pagination.Page - 1;
            var takeCount = pagination.ItemsPerPage;

            return initialQuery.Skip(page * takeCount).Take(takeCount);
        }

    }
}
