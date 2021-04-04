using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.DAL.Repositories
{
    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(IContext context) : base(context)
        {

        }
    }
}
