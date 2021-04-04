using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.Models
{
    public interface IModel
    {
        int Id { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        int CreatedBy { get; set; }
        int ModifiedBy { get; set; }
        bool IsActive { get; set; }
    }
}
