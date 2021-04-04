using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;
using FavoritesSurvey.Core.Architecture.BLL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters
{
    public class GetResponseStatsParameter : IRequest<IServiceResult<IEnumerable<ComputedResponseVM>>>
    {
    }
}
