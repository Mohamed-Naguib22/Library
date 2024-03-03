using Microsoft.AspNetCore.Mvc;
using Library.Application.Dtos.AuthDtos;
using MediatR;
using Library.Application.Commands.AuthCommands;
using Library.Application.Queries.AuthQueries;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAysnc([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RegisterCommand(registerDto);

            var result = await _mediator.Send(command);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
            //return result.Succeeded ? Ok("Please check your email to verify your account") : BadRequest(result.Message);
        }

        [HttpPost("verify-accout")]
        public async Task<IActionResult> VerifyAccountAsync([FromBody] VerifyAccountDto verifyAccountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new VerifyAccountQuery(verifyAccountDto);

            var result = await _mediator.Send(query);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new LoginQuery(loginDto);

            var result = await _mediator.Send(query);
            
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("refreshToken");

            return Ok("Logged out successfully");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
        {
            var refreshToken = GetRefreshTokenFromCookie();

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new ChangePasswordCommand(changePasswordDto, refreshToken);

            var result = await _mediator.Send(command);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = GetRefreshTokenFromCookie();

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new RefreshTokenCommand(refreshToken);

            var result = await _mediator.Send(command);
            
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeDto model)
        {
            var refreshToken = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required!");

            var command = new RevokeTokenCommand(refreshToken);

            var result = await _mediator.Send(command);

            return result ? Ok("Token revoked successfully") : BadRequest("Token is invalid!");
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        private string GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refreshToken"];
        }
    }
}
