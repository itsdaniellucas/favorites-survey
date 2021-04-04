using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;
using FavoritesSurvey.Core.Architecture.BLL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters
{
    public class SubmitResponseParameter : IRequest<IServiceResult>
    {
        public string Guid { get; set; }
        public List<ResponseVM> Responses { get; set; } = new List<ResponseVM>();
    }
}
