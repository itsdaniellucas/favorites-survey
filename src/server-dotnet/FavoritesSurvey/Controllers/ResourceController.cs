using FavoritesSurvey.BLL.Services.ResourceService.Models.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesSurvey.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        IMediator _mediator;
        IRedisClientsManager _redisMgr;

        public ResourceController(IMediator mediator, IRedisClientsManager redisMgr)
        {
            _mediator = mediator;
            _redisMgr = redisMgr;
        }

        [HttpGet]
        [Route("Questions")]
        public async Task<IActionResult> GetQuestions()
        {
            return Ok(await _mediator.Send(new GetQuestionsParameter()));
        }

        [HttpGet]
        [Route("Answers")]
        public async Task<IActionResult> GetAnswers()
        {
            return Ok(await _mediator.Send(new GetAnswersParameter()));
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> GetTest()
        {
            var value = 25;
            var redis = await _redisMgr.GetClientAsync();
            var valueFromCache = await redis.GetAsync<int>("test");
            if (valueFromCache == default(int))
            {
                valueFromCache = value;
                await redis.SetAsync("test", valueFromCache);
            }    
            return Ok(valueFromCache);
        }
    }
}
