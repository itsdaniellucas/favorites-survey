using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FavoritesSurvey.DAL.Repositories
{
    public class ComputedResponseRepository : Repository<ComputedResponse>
    {
        public ComputedResponseRepository(IContext context) : base(context)
        {

        }

        protected override IQueryable<ComputedResponse> AttachIncludes(DbSet<ComputedResponse> dbSet)
        {
            return dbSet.Include(i => i.Question)
                        .Include(i => i.Answer);
        }
    }
}
