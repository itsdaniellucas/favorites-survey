using FavoritesSurvey.Core.Architecture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FavoritesSurvey.Models
{
    public class Survey : IModel
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Guid { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }


        public ICollection<Response> Responses { get; set; }
    }
}
