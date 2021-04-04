using FavoritesSurvey.BLL.Services.ResourceService.Models.ViewModels;
using FavoritesSurvey.Core.Architecture.BLL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.ResourceService.Models.Parameters
{
    public class GetQuestionsParameter : IRequest<IServiceResult<IEnumerable<QuestionVM>>>
    {
    }
}
