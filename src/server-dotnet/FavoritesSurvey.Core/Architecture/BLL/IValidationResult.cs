using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public interface IValidationResult
    {
        object Data { get; set; }
        bool IsSuccessful { get; set; }
        string Error { get; set; }
    }

    public interface IValidationResult<T> : IValidationResult
    {
        new T Data { get; set; }
    }
}
