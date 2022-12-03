using Application.Accounts.Commands;
using Application.Dtos;
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
    }
}
