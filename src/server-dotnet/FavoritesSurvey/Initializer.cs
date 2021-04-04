using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.BLL.Services.ResourceService.Models.ViewModels;
using FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters;
using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;
using FavoritesSurvey.BLL.Services.SurveyService.Validators;
using FavoritesSurvey.Core;
using FavoritesSurvey.Core.Architecture.BLL;
using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.DAL;
using FavoritesSurvey.DAL.Repositories;
using FavoritesSurvey.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorConfig = FavoritesSurvey.Core.Error;

namespace FavoritesSurvey
{
    public class Initializer
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var env = configuration["Environment"];
            services.AddSingleton<IRedisClientsManager>(i => new RedisManagerPool(configuration[$"Services:Redis:{env}"]));
            services.AddSingleton<IConnectionFactory>(i => new ConnectionFactory() { DispatchConsumersAsync = true, Uri = new Uri(configuration[$"Services:RabbitMq:{env}"])});
            
            // Misc
            services.AddScoped<ICache, Cache>(); // Redis wrapper
            services.AddScoped<IQueue, Queue>(); // RabbitMq wrapper

            // Context
            services.AddScoped<IContext, FavoritesSurveyContext>();

            // Repos
            services.AddScoped<IRepository<Answer>, AnswerRepository>();
            services.AddScoped<IRepository<ComputedResponse>, ComputedResponseRepository>();
            services.AddScoped<IRepository<Question>, QuestionRepository>();
            services.AddScoped<IRepository<Response>, ResponseRepository>();
            services.AddScoped<IRepository<Survey>, SurveyRepository>();

            // SurveyService
            services.AddScoped<SurveyValidator>();
        }

        public static void ConstructDatabase(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = (scope.ServiceProvider.GetRequiredService<IContext>() as DbContext);
                context.Database.EnsureCreated();
            }
        }

        public static void RegisterErrors()
        {
            ErrorConfig.Register(ErrorTypes.APIFailure, "Associated API(s) for this feature have failed or have errors");
            ErrorConfig.Register(ErrorTypes.LoginFailure, "You are unauthorized to access this application.");
            ErrorConfig.Register(ErrorTypes.ConcurrentOperation, "Another operation with the same data is being done, please try again later.");
            ErrorConfig.Register(ErrorTypes.UserRoleInvalid, "You are unauthorized to perform this action due to role restrictions.");
            ErrorConfig.Register(ErrorTypes.RecordExists, "{0} already exists. {1}");
            ErrorConfig.Register(ErrorTypes.RecordNotExists, "{0} does not exist.");
            ErrorConfig.Register(ErrorTypes.RecordInvalid, "{0} is invalid. {1}");
            ErrorConfig.Register(ErrorTypes.FieldOutOfRange, "{0} is out of range. Must be between {1} and {2}");
            ErrorConfig.Register(ErrorTypes.FieldTooLong, "{0} is too long. Must be less than {1}");
            ErrorConfig.Register(ErrorTypes.FieldLessThanLimit, "{0} is smaller than the limit. Must be greater than {1}");
            ErrorConfig.Register(ErrorTypes.FieldGreaterThanLimit, "{0} is bigger than the limit. Must be less than {1}");
            ErrorConfig.Register(ErrorTypes.FieldRequired, "{0} is required");
        }

        public static void RegisterMappings()
        {
            Mapper.Register<SubmitResponseParameter, Survey>(i => new Survey()
            {
                Guid = i.Guid
            });

            Mapper.Register<ResponseVM, Response>(i => new Response()
            {
                QuestionId = i.QuestionId,
                AnswerId = i.AnswerId,
                SurveyId = i.SurveyId,
            });

            Mapper.Register<Answer, AnswerVM>(i => new AnswerVM()
            {
                Id = i.Id,
                QuestionId = i.QuestionId,
                Name = i.Name,
            });

            Mapper.Register<Question, QuestionVM>(i => new QuestionVM()
            {
                Id = i.Id,
                Name = i.Name,
            });

            Mapper.Register<ComputedResponse, ComputedResponseVM>(i => new ComputedResponseVM()
            {
                QuestionId = i.QuestionId,
                AnswerId = i.AnswerId,
                Count = i.Count,
                Total = i.Total,
                Rate = i.Rate
            });
        }
    }
}
