using Application.Accounts.Commands;
using Application.Dtos;
using Application.Interfaces;
using Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WorkflowApi.Controllers
{
    //[AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterCommand command)
        {
            //var cookieOptions = new CookieOptions();
            //var c = Request.Cookies["cookieName"];
            //var cookie = Request.Cookies.Get("cookieName");

            return await _mediator.Send(command);
            //return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginCommand command)
        {
            //Request.Cookies.;

            //Response.Cookies.
            return await _mediator.Send(command);
            //return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            //var response = _userService.RefreshToken(refreshToken, ipAddress());
            //setTokenCookie(response.RefreshToken);
            return await _mediator.Send(new RefreshTokenCommand());

        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _userService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }




    }
}
