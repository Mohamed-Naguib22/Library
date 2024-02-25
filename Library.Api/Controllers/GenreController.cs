using Library.Application.Commands.GenreCommands;
using Library.Application.Dtos.GenreDto;
using Library.Application.Queries.GenreQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var query = new GetAllGenresQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGenreAsync([FromForm] AddGenreDto genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var command = new CreateGenreCommand(genreDto);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete/{genreId}")]
        public async Task<IActionResult> DeleteBookAsync(int genreId)
        {
            var command = new DeleteGenreCommand(genreId);

            var result = await _mediator.Send(command);

            return result ? Ok("Genre deleted successfully") : NotFound("This genre is not found");
        }
    }
}
