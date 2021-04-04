using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters;
using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;
using FavoritesSurvey.BLL.Services.SurveyService.Validators;
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

namespace FavoritesSurvey.BLL.Services.SurveyService.Handlers.Commands
{
    public class SubmitResponseHandler : IRequestHandler<SubmitResponseParameter, IServiceResult>
    {
        IContext _context;
        IQueue _queue;
        IRepository<Survey> _surveyRepo;
        IRepository<Response> _responseRepo;
        SurveyValidator _validator;

        public SubmitResponseHandler(IContext context,
                                    IQueue queue,
                                    IRepository<Survey> surveyRepo,
                                    IRepository<Response> responseRepo,
                                    SurveyValidator validator)
        {
            _context = context;
            _queue = queue;
            _surveyRepo = surveyRepo;
            _responseRepo = responseRepo;
            _validator = validator;
        }

        public async Task<IServiceResult> Handle(SubmitResponseParameter request, CancellationToken cancellationToken)
        {
            var contextUserId = 0;
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsSuccessful)
                return ServiceResult.CreateError(validation.Error);

            await _context.RunTransactionAsync(async () =>
            {
                var survey = Mapper.Map<SubmitResponseParameter, Survey>(request);

                _surveyRepo.Insert(survey, contextUserId);

                await _context.CommitAsync();

                foreach(var r in request.Responses)
                {
                    r.SurveyId = survey.Id;
                    var response = Mapper.Map<ResponseVM, Response>(r);
                    _responseRepo.Insert(response, contextUserId);
                }
            });

            _queue.Publish(new QueueMessage()
            {
                Guid = request.Guid,
                Timestamp = DateTime.Now
            });

            return ServiceResult.CreateSuccess();
        }
    }
}
