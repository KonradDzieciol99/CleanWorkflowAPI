using Application.Interfaces;
using Core.Interfaces.IRepositories;
using Domain.Entities;
using Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowApi.Exceptions;

namespace Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpContext _httpContext;

        public RefreshTokenService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
            _httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentNullException("empty httpContext");
        }

        public async Task<Guid> CreateRefreshToken(AppUser appUser)
        {
            var refreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid(),
                UserId = appUser.Id
            };

            _unitOfWork.RefreshTokensRepository.Add(refreshToken);

            if (await _unitOfWork.Complete())
            {
                return refreshToken.Token;
            }

            throw new Exception("error on CreateRefreshToken");

        }

        public Guid GetRefreshTokenFromCookie()
        {
            string? refreshTokenAsString=_httpContext.Request.Cookies["Workflow-Refresh-Token"];

            if (string.IsNullOrEmpty(refreshTokenAsString))
            {
                throw new BadRequestException("empty cookie");
            }

            if (Guid.TryParse(refreshTokenAsString, out Guid refreshTokenAsGuid))
            {
                return refreshTokenAsGuid;
            }

            throw new BadRequestException("bad format of cookie");
        }

        public void SetRefreshTokenInCookie(Guid refreshToken)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1),
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true
            };

            try
            {
                _httpContext.Response.Cookies.Append("Workflow-Refresh-Token", refreshToken.ToString(), cookieOptions);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task RevokeRefreshToken(Guid refreshToken, AppUser appUser)
        {
            var refreshTokenFromDb  = await _unitOfWork.RefreshTokensRepository.FindOneAsync(x => x.Token == refreshToken);
            if (refreshTokenFromDb == null)
            {
                throw new BadRequestException("RefreshToken does not exist");
            }
            refreshTokenFromDb.IsRevoked = true;

            if (await _unitOfWork.Complete())
            {
                await Task.CompletedTask;
                return;
            }

            throw new BadRequestException("Can't revoke RefreshToken");
        }
    }
}
