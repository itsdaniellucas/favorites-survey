using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.SVC.ResponseStats.Models
{
    public class ResponseGroupCount
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Count { get; set; }
    }
}
