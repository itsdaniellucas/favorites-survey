using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FavoritesSurvey.DAL.Repositories
{
    public class AnswerRepository : Repository<Answer>
    {
        public AnswerRepository(IContext context) : base(context)
        {

        }

        protected override IQueryable<Answer> AttachIncludes(DbSet<Answer> dbSet)
        {
            return dbSet.Include(i => i.Question);
        }
    }
}
