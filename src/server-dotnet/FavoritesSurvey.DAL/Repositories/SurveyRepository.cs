using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.DAL.Repositories
{
    public class SurveyRepository : Repository<Survey>
    {
        public SurveyRepository(IContext context) : base(context)
        {

        }
    }
}
