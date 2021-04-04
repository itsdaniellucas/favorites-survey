using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public interface IValidator<TInput>
    {
        IValidationResult Validate(TInput target);
        Task<IValidationResult> ValidateAsync(TInput target);
    }

    public interface IValidator<TOutput, TInput>
    {
        IValidationResult<TOutput> Validate(TInput target);
        Task<IValidationResult<TOutput>> ValidateAsync(TInput target);
    }
}
