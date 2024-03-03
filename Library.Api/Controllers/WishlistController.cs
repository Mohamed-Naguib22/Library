using Library.Application.Commands.WishlistCommands;
using Library.Application.Dtos.WishlistDtos;
using Library.Application.Queries.WishlistQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetWishlistAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var query = new GetWishlistQuery(refreshToken);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlistAsync([FromBody] AddToWishlistDto addToWishlistDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new AddToWishlistCommand(addToWishlistDto, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("remove/{bookId}")]
        public async Task<IActionResult> RemoveFromWishlistAsync(int bookId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new RemoveFromWishlistCommand(bookId, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
