using Library.Application.Commands.BookCommands;
using Library.Application.Dtos.BookDtos;
using Library.Application.Queries.BookQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var query = new GetAllBooksQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("get/{bookId}")]
        public async Task<IActionResult> GetBookAsync(int bookId)
        {
            var query = new GetBookQuery(bookId);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookAsync([FromForm] AddBookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateBookCommand(bookDto);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update/{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(int bookId, [FromForm] UpdateBookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateBookCommand(bookId, bookDto);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            var command = new DeleteBookCommand(bookId);

            var result = await _mediator.Send(command);

            return result ? Ok("Book deleted successfully") : NotFound("This book is not found");
        }
    }
}
