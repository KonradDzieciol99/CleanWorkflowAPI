using Application.Accounts.Commands;
using Application.Dtos;
using Application.Interfaces;
using Core.Interfaces;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Handlers
{
    public class RegisterCommandHandler:IRequestHandler<RegisterCommand,UserDto>
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public RegisterCommandHandler(IMediator mediator, IJwtTokenService jwtTokenService, IIdentityService identityService,IRefreshTokenService refreshTokenService)
        {
            this._mediator = mediator;
            this._jwtTokenService = jwtTokenService;
            this._identityService = identityService;
            this._refreshTokenService = refreshTokenService;
        }

        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var user = await _identityService.CreateUserAsync(request.Email,request.Password);

            await _mediator.Publish(new UserCreatedEvent(user));

            var refreshToken = await _refreshTokenService.CreateRefreshToken(user);
            _refreshTokenService.SetRefreshTokenInCookie(refreshToken);

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.CreateToken(user),
            };
        }
    }
}
