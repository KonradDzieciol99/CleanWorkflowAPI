using Application.Accounts.Commands;
using Application.Dtos;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Domain.Entities;
using Domain.Identity.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Handlers
{
    internal class RefreshTokenCommandHandler:IRequestHandler<RefreshTokenCommand, UserDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(IIdentityService identityService, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService,IUnitOfWork unitOfWork)
        {
            this._identityService = identityService;
            this._jwtTokenService = jwtTokenService;
            this._refreshTokenService = refreshTokenService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var endOfLifeRefreshToken = _refreshTokenService.GetRefreshTokenFromCookie();
            var user = await _identityService.FindRefreshTokenOwner(endOfLifeRefreshToken);
            await _refreshTokenService.RevokeRefreshToken(endOfLifeRefreshToken,user);

            var newRefreshToken = await _refreshTokenService.CreateRefreshToken(user);
            var jwtToken = await _jwtTokenService.CreateToken(user);
            _refreshTokenService.SetRefreshTokenInCookie(newRefreshToken);

            //return jwtToken;
            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.CreateToken(user),
            };
        }
    }
}
