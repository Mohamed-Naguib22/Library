using Library.Application.Commands.CartCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Queries.CartQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCartAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var query = new GetCartQuery(refreshToken);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartDto addToCartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new AddToCartCommand(addToCartDto, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("remove/{bookId}")]
        public async Task<IActionResult> RemoveFromCartAsync(int bookId)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new RemoveFromCartCommand(bookId, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut("change-quantity")]
        public async Task<IActionResult> ChangeQuantityAsync([FromBody] ChangeQuantityDto updateQuantityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new ChangeItemQuantityCommand(updateQuantityDto, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }
    }
}
