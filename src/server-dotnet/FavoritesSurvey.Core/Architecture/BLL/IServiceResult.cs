using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public interface IServiceResult
    {
        object Data { get; set; }
        bool IsSuccessful { get; set; }
        string Error { get; set; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        new T Data { get; set; }
    }
}
