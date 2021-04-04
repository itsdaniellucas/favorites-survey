using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.Core.Architecture.BLL;
using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using FavoritesSurvey.SVC.ResponseStats.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FavoritesSurvey.BLL.Services.SurveyService.Models.ViewModels;

namespace FavoritesSurvey.SVC.ResponseStats
{
    public class Worker : BackgroundService
    {
        ICache _cache;
        IQueue _queue;
        IServiceProvider _serviceProvider;
        

        public Worker(ICache cache,
                      IQueue queue,
                      IServiceProvider serviceProvider)
        {
            _cache = cache;
            _queue = queue;
            _serviceProvider = serviceProvider;
            _queue.RegisterConsumer<QueueMessage>(ProcessResponseData);
        }

        private async Task ProcessResponseData(QueueMessage message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContext>();
                var _responseRepo = scope.ServiceProvider.GetRequiredService<IRepository<Response>>();
                var _computedRepo = scope.ServiceProvider.GetRequiredService<IRepository<ComputedResponse>>();

                // group by question + answer to get count for each answer for that specific question
                var responseGroups = await _responseRepo.AsQuery()
                                                .Where(i => i.IsActive)
                                                .GroupBy(i => new { i.QuestionId, i.AnswerId })
                                                .Select(i => new ResponseGroupCount()
                                                {
                                                    QuestionId = i.Key.QuestionId,
                                                    AnswerId = i.Key.AnswerId,
                                                    Count = i.Count(),
                                                })
                                                .ToListAsync();

                var responseGroupMap = responseGroups.ToDictionaryGroup(i => i.QuestionId, i => i);

                List<ComputedResponse> computedResponses = new List<ComputedResponse>();

                foreach (var rg in responseGroupMap) // group by question to get total and rate
                {
                    var total = rg.Value.Sum(i => i.Count);

                    foreach (var r in rg.Value)
                    {
                        computedResponses.Add(new ComputedResponse()
                        {
                            QuestionId = r.QuestionId,
                            AnswerId = r.AnswerId,
                            Count = r.Count,
                            Total = total,
                            Rate = (double)r.Count / total,
                        });
                    }
                }

                await _context.RunTransactionAsync(async () =>
                {
                    var contextUserId = 0;
                    foreach (var cr in computedResponses)
                        _computedRepo.Upsert(
                            criteria: i => i.QuestionId == cr.QuestionId && i.AnswerId == cr.AnswerId,
                            contextUserId: contextUserId,
                            item: cr,
                            changes: i =>
                            {
                                i.Count = cr.Count;
                                i.Total = cr.Total;
                                i.Rate = cr.Rate;
                            }
                        );
                    await Task.CompletedTask;
                });


                // set to redis afterwards
                string key = typeof(IServiceResult<IEnumerable<ComputedResponseVM>>).FullName;
                var cast = computedResponses.Select(i => new ComputedResponseVM()
                {
                    QuestionId = i.QuestionId,
                    AnswerId = i.AnswerId,
                    Count = i.Count,
                    Rate = i.Rate,
                    Total = i.Total,
                });
                IServiceResult<IEnumerable<ComputedResponseVM>> cachedResult = ServiceResult.CreateSuccess(cast); // declare type to preserve it
                await _cache.SetAsync(key, cachedResult);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            _cache.Dispose();
            _queue.Dispose();
            _queue = null;
            _cache = null;
            base.Dispose();
        }
    }
}
