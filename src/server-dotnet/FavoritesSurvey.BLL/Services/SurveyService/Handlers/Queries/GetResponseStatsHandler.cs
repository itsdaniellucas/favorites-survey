using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters;
using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;
using FavoritesSurvey.Core;
using FavoritesSurvey.Core.Architecture.BLL;
using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FavoritesSurvey.BLL.Services.SurveyService.Handlers.Queries
{
    public class GetResponseStatsHandler : IRequestHandler<GetResponseStatsParameter, IServiceResult<IEnumerable<ComputedResponseVM>>>
    {
        ICache _cache;
        IRepository<ComputedResponse> _computedRepo;

        public GetResponseStatsHandler(IRepository<ComputedResponse> computedRepo, ICache cache)
        {
            _computedRepo = computedRepo;
            _cache = cache;
        }

        public async Task<IServiceResult<IEnumerable<ComputedResponseVM>>> Handle(GetResponseStatsParameter request, CancellationToken cancellationToken)
        {
            string key = typeof(IServiceResult<IEnumerable<ComputedResponseVM>>).FullName;
            var cachedResult = await _cache.GetAsync<IServiceResult<IEnumerable<ComputedResponseVM>>>(key);

            if (cachedResult == null)
            {
                var answers = await _computedRepo.GetAllActiveAsync();
                var cast = Mapper.Map<ComputedResponse, ComputedResponseVM>(answers);
                cachedResult = ServiceResult.CreateSuccess(cast);
                await _cache.SetAsync(key, cachedResult);
            }

            return cachedResult;
        }
    }
}
