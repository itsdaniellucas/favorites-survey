using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Services.ResourceService.Models.ViewModels
{
    public class AnswerVM
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
    }
}
