using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FavoritesSurvey.DAL.Repositories
{
    public class ResponseRepository : Repository<Response>
    {
        public ResponseRepository(IContext context) : base(context)
        {

        }

        protected override IQueryable<Response> AttachIncludes(DbSet<Response> dbSet)
        {
            return dbSet.Include(i => i.Question)
                        .Include(i => i.Answer)
                        .Include(i => i.Survey);
        }
    }
}
