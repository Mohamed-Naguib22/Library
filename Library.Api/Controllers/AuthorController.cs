using Library.Application.Commands.AuthorCommands;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Queries.AuthorQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{authorId}")]
        public async Task<IActionResult> GetAuthorAsync(int authorId)
        {
            var query = new GetAuthorQuery(authorId);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            var query = new GetAllAuthorsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAuthorAsync([FromForm] AddAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var command = new CreateAuthorCommand(authorDto);

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("update/{authorId}")]
        public async Task<IActionResult> UpdateAuthorAsync(int authorId, [FromForm] UpdateAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var command = new UpdateAuthorCommand(authorId, authorDto);

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                    return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("delete/{authorId}")]
        public async Task<IActionResult> DeleteAuthorAsync(int authorId)
        {
            var command = new DeleteAuthorCommand(authorId);

            var result = await _mediator.Send(command);

            return result ? Ok("Deleted successfully") : NotFound("Author is not found");
        }
    }
}
