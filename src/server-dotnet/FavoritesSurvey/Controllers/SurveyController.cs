using FavoritesSurvey.BLL.Services.SurveyService.Models.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesSurvey.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        IMediator _mediator;
        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Stats")]
        public async Task<IActionResult> GetResponseStats()
        {
            return Ok(await _mediator.Send(new GetResponseStatsParameter()));
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> SubmitSurvey(SubmitResponseParameter parameter)
        {
            parameter.Guid = Guid.NewGuid().ToString("N");
            return Ok(await _mediator.Send(parameter));
        }
    }
}
