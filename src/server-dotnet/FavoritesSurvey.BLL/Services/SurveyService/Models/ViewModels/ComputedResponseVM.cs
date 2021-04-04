using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels
{
    public class ComputedResponseVM
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public double Rate { get; set; }
    }
}
