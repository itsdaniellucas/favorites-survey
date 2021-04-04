using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.Models
{
    public interface IModelName : IModel
    {
        string Name { get; set; }
    }
}
