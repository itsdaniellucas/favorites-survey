using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public class ValidationResult : IValidationResult
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }

        public static ValidationResult Create(bool success)
        {
            var result = new ValidationResult()
            {
                Data = default(object),
                IsSuccessful = success,
                Error = string.Empty
            };
            return result;
        }

        public static ValidationResult CreateError(string error, object data = default(object))
        {
            var result = ValidationResult.Create(false);
            result.Error = error;
            result.Data = data;
            return result;
        }

        public static ValidationResult CreateSuccess(object data = default(object))
        {
            var result = ValidationResult.Create(true);
            result.Data = data;
            return result;
        }
    }

    public class ValidationResult<T> : IValidationResult<T>, IValidationResult
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
        object IValidationResult.Data { get; set; }

        public static ValidationResult<T> Create(bool success)
        {
            var result = new ValidationResult<T>()
            {
                Data = default(T),
                IsSuccessful = success,
                Error = string.Empty
            };
            (result as IValidationResult).Data = default(T);
            return result;
        }

        public static ValidationResult<T> CreateError(string error, T data = default(T))
        {
            var result = ValidationResult<T>.Create(false);
            result.Error = error;
            result.Data = data;
            (result as IValidationResult).Data = data;
            return result;
        }

        public static ValidationResult<T> CreateSuccess(T data = default(T))
        {
            var result = ValidationResult<T>.Create(true);
            result.Data = data;
            (result as IValidationResult).Data = data;
            return result;
        }
    }
}
