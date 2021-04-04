using FavoritesSurvey.Core.Architecture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.Core.Architecture.DAL
{
    public interface IContext
    {
        DbSet<T> Get<T>() where T : class, IModel;
        void Commit();
        Task CommitAsync();
        void RunTransaction(Action transaction);
        Task RunTransactionAsync(Func<Task> transaction);
        DbContext GetContext();
    }
}
