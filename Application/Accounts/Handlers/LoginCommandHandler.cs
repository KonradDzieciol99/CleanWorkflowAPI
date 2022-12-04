using Application.Accounts.Commands;
using Application.Dtos;
using Application.Interfaces;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginCommandHandler(IIdentityService identityService,IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
        {
            this._identityService = identityService;
            this._jwtTokenService = jwtTokenService;
            this._refreshTokenService = refreshTokenService;
        }
        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.SignInAsync(request.Email, request.Password);

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
