using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels
{
    public class ResponseVM
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int SurveyId { get; set; }
    }
}
