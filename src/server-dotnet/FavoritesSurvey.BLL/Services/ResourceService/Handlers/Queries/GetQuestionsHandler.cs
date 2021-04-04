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
    public class GetQuestionsHandler : IRequestHandler<GetQuestionsParameter, IServiceResult<IEnumerable<QuestionVM>>>
    {
        IRepository<Question> _questionRepo;
        ICache _cache;

        public GetQuestionsHandler(IRepository<Question> questionRepo, ICache cache)
        {
            _cache = cache;
            _questionRepo = questionRepo;
        }
        public async Task<IServiceResult<IEnumerable<QuestionVM>>> Handle(GetQuestionsParameter request, CancellationToken cancellationToken)
        {
            string key = typeof(IServiceResult<IEnumerable<QuestionVM>>).FullName;
            var cachedResult = await _cache.GetAsync<IServiceResult<IEnumerable<QuestionVM>>>(key);

            if(cachedResult == null)
            {
                var questions = await _questionRepo.GetAllActiveAsync();
                var cast = Mapper.Map<Question, QuestionVM>(questions);
                cachedResult = ServiceResult.CreateSuccess(cast);
                await _cache.SetAsync(key, cachedResult);
            }
            
            return cachedResult;
        }
    }
}
