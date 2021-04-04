using FavoritesSurvey.Core.Architecture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Models
{
    public class ComputedResponse : IModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

        public int Count { get; set; }
        public int Total { get; set; }
        public double Rate { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }


        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
