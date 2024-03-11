using Library.Application.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> OrderHistory()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var query = new GetUserOrdersQuery(refreshToken);
            
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
