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
            return await _mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            return await _mediator.Send(new RefreshTokenCommand());
        }

        //[HttpPost("revoke-token")]
        //public IActionResult RevokeToken(RevokeTokenRequest model)
        //{
        //    // accept refresh token in request body or cookie
        //    var token = model.Token ?? Request.Cookies["refreshToken"];

        //    if (string.IsNullOrEmpty(token))
        //        return BadRequest(new { message = "Token is required" });

        //    _userService.RevokeToken(token, ipAddress());
        //    return Ok(new { message = "Token revoked" });
        //}




    }
}
