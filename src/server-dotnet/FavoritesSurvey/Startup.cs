using FavoritesSurvey.Misc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace FavoritesSurvey
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Initializer.RegisterDependencies(services, Configuration);

            Initializer.RegisterErrors();

            Initializer.RegisterMappings();

            services.AddMediatR(AssemblyLoadContext.Default.Assemblies.ToArray());

            services.AddSignalR().AddJsonProtocol(x =>
            {
                x.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddControllers(x =>
            {
                x.Filters.Add(typeof(LoggingFilter));
                x.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            }).AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddCors(x => x.AddPolicy("DefaultPolicy", builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Initializer.ConstructDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("DefaultPolicy");

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<SignalRHub>("/api/v1/SignalR");
            });
        }
    }
}
