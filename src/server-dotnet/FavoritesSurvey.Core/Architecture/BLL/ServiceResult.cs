using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public class ServiceResult : IServiceResult
    {
        public object Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }

        public static ServiceResult Create(bool success)
        {
            var result = new ServiceResult()
            {
                Data = default(object),
                IsSuccessful = success,
                Error = string.Empty,
            };
            return result;
        }

        public static ServiceResult CreateSuccess(object data = default(object))
        {
            var result = ServiceResult.Create(true);
            result.Data = data;
            return result;
        }

        public static ServiceResult<T> CreateSuccess<T>(T data = default(T))
        {
            return ServiceResult<T>.CreateSuccess(data);
        }

        public static ServiceResult CreateError(string error = "")
        {
            var result = ServiceResult.Create(false);
            result.Error = error;
            return result;
        }

        public static ServiceResult<T> CreateError<T>(string error = "")
        {
            return ServiceResult<T>.CreateError(error);
        }
    }

    public class ServiceResult<T> : IServiceResult<T>, IServiceResult
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        object IServiceResult.Data { get; set; }

        public static ServiceResult<T> Create(bool success)
        {
            var result = new ServiceResult<T>()
            {
                Data = default(T),
                IsSuccessful = success,
                Error = string.Empty,
            };
            (result as IServiceResult).Data = default(T);
            return result;
        }

        public static ServiceResult<T> CreateSuccess(T data = default(T))
        {
            var result = ServiceResult<T>.Create(true);
            result.Data = data;
            (result as IServiceResult).Data = data;
            return result;
        }

        public static ServiceResult<T> CreateError(string error = "")
        {
            var result = ServiceResult<T>.Create(false);
            result.Error = error;
            return result;
        }
    }
}
