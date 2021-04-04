using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.BLL.Services.ResourceService.Models.Parameters;
using FavoritesSurvey.BLL.Services.ResourceService.Models.ViewModels;
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

namespace FavoritesSurvey.BLL.Services.ResourceService.Handlers.Queries
{
    public class GetAnswersHandler : IRequestHandler<GetAnswersParameter, IServiceResult<IEnumerable<AnswerVM>>>
    {
        IRepository<Answer> _answerRepo;
        ICache _cache;

        public GetAnswersHandler(IRepository<Answer> answerRepo, ICache cache)
        {
            _cache = cache;
            _answerRepo = answerRepo;
        }

        public async Task<IServiceResult<IEnumerable<AnswerVM>>> Handle(GetAnswersParameter request, CancellationToken cancellationToken)
        {
            string key = typeof(IServiceResult<IEnumerable<AnswerVM>>).FullName;
            var cachedResult = await _cache.GetAsync<IServiceResult<IEnumerable<AnswerVM>>>(key);

            if(cachedResult == null)
            {
                var answers = await _answerRepo.GetAllActiveAsync();
                var cast = Mapper.Map<Answer, AnswerVM>(answers);
                cachedResult = ServiceResult.CreateSuccess(cast);
                await _cache.SetAsync(key, cachedResult);
            }
            
            return cachedResult;
        }
    }
}
