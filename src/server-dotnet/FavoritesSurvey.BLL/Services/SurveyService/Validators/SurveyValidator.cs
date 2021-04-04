using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters;
using FavoritesSurvey.Core;
using FavoritesSurvey.Core.Architecture.BLL;
using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.BLL.Services.SurveyService.Validators
{
    public class SurveyValidator : IValidator<SubmitResponseParameter>
    {
        IRepository<Answer> _answerRepo;
        IRepository<Question> _questionRepo;

        public SurveyValidator(IRepository<Answer> answerRepo,
                                IRepository<Question> questionRepo)
        {
            _answerRepo = answerRepo;
            _questionRepo = questionRepo;
        }

        public IValidationResult Validate(SubmitResponseParameter target)
        {
            throw new NotImplementedException();
        }

        public async Task<IValidationResult> ValidateAsync(SubmitResponseParameter target)
        {
            var answers = await _answerRepo.GetAllActiveAsync();
            var questions = await _questionRepo.GetAllActiveAsync();

            var questionIds = questions.Select(i => i.Id).ToHashSet();
            var answersMap = answers.ToDictionary(i => i.Id, i => i);

            var responses = target.Responses;

            var incompleteSurvey = responses.Count != questionIds.Count;

            if (incompleteSurvey)
                return ValidationResult.CreateError(Error.For(ErrorTypes.RecordInvalid, ErrorConstants.Survey));

            foreach (var res in responses)
            {
                Answer resAnswer = null;
                var invalidQuestion = !questionIds.Contains(res.QuestionId);
                var invalidAnswer = !answersMap.TryGetValue(res.AnswerId, out resAnswer);
                var invalidAnswerToAQuestion = resAnswer.QuestionId != res.QuestionId;

                if (invalidQuestion || invalidAnswer || invalidAnswerToAQuestion)
                    return ValidationResult.CreateError(Error.For(ErrorTypes.RecordInvalid, ErrorConstants.Survey));
            }

            return ValidationResult.CreateSuccess();
        }
    }
}
