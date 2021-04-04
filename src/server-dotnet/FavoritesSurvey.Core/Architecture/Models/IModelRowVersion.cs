using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.Models
{
    public interface IModelRowVersion : IModel
    {
        byte[] RowVersion { get; set; }
    }
}
