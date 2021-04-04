using FavoritesSurvey.BLL.Misc;
using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.DAL;
using FavoritesSurvey.DAL.Repositories;
using FavoritesSurvey.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesSurvey.SVC.ResponseStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Delay(10000).Wait(); // wait 10 seconds before run
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;
                    var env = config["Environment"];
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IRedisClientsManager>(i => new RedisManagerPool(config[$"Services:Redis:{env}"]));
                    services.AddSingleton<IConnectionFactory>(i => new ConnectionFactory() { DispatchConsumersAsync = true, Uri = new Uri(config[$"Services:RabbitMq:{env}"]) });
                    services.AddSingleton<ICache, Cache>(); // Redis wrapper
                    services.AddSingleton<IQueue, Queue>(); // RabbitMq wrapper

                    // Context
                    services.AddScoped<IContext, FavoritesSurveyContext>();

                    // Repos
                    services.AddScoped<IRepository<ComputedResponse>, ComputedResponseRepository>();
                    services.AddScoped<IRepository<Response>, ResponseRepository>();
                })
                .UseWindowsService()
                .UseSystemd();
    }
}
