using Library.Application.Queries.CartQueries;
using Library.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search-history")]
        public async Task<IActionResult> GetSearchHistoryAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var query = new GetSearchHistoryQuery(refreshToken);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
