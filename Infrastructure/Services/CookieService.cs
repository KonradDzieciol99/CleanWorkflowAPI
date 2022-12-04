using Application.Interfaces;
using Core.Interfaces.IRepositories;
using Domain.Entities;
using Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Token = new Guid(),
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
            string? refreshTokenAsString=_httpContext.Request.Cookies["sdfs"];

            if (string.IsNullOrEmpty(refreshTokenAsString))
            {
                throw new ArgumentNullException("empty cookie");//badrequest???
            }

            
            if (Guid.TryParse(refreshTokenAsString, out Guid refreshTokenAsGuid))
            {
                return refreshTokenAsGuid;
            }

            throw new FormatException("bad format of cookie");//badrequest???
        }

        public void SetRefreshTokenInCookie(Guid refreshToken)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7), // one week expiry time
            };

            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.ToString(), cookieOptions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
