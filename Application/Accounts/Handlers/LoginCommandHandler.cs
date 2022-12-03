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

        public LoginCommandHandler(IIdentityService identityService,IJwtTokenService jwtTokenService)
        {
            this._identityService = identityService;
            this._jwtTokenService = jwtTokenService;
        }
        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.SignInAsync(request.Email, request.Password);

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.CreateToken(user),
            };
        }
    }
}
