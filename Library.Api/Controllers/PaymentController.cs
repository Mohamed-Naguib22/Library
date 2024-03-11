using Microsoft.AspNetCore.Mvc;
using Library.Application.Dtos.OrderDtos;
using MediatR;
using Library.Application.Commands.OrderCommands;
using Library.Domain.Models;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutAsync([FromBody] IEnumerable<OrderItemDto> orderItems)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new CheckoutCommand(orderItems, refreshToken);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }
        //https://localhost:7285/Order/OrderConfirmation?orderId=3
        [HttpGet("OrderConfirmation")]
        public async Task<IActionResult> ConfirmOrder([FromQuery] int orderId)
        {
            var command = new OrderConfirmationCommand(orderId);

            var result = await _mediator.Send(command);

            return result ? Ok("Order placed successfully") : NotFound("Order is not found");
        }
    }
}
