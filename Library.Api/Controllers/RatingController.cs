using Library.Application.Commands.RatingCommands;
using Library.Application.Dtos.RatingDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateAsync([FromBody] RateDto rateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken =  Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new RateCommand(rateDto, refreshToken);

            var result = await _mediator.Send(command);

            return string.IsNullOrEmpty(result) ? Ok("Book rated successfully") : BadRequest(result);
        }
    }
}
