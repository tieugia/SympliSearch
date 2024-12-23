using MediatR;
using Microsoft.AspNetCore.Mvc;
using SympliSearch.Application.Queries.Bing;
using SympliSearch.Application.Queries.Google;
using System.ComponentModel.DataAnnotations;

namespace SympliSearch.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("google")]
        public async Task<IActionResult> GetRanksFromGoogle([FromQuery][Required] GoogleRankingQuery googleRankingRequest)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(await _mediator.Send(googleRankingRequest));
        }

        [HttpGet]
        [Route("bing")]
        public async Task<IActionResult> GetRanksFromBing([FromQuery][Required] BingRankingQuery bingRankingRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _mediator.Send(bingRankingRequest));
        }
    }
}
