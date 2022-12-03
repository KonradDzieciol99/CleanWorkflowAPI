using Application.Interfaces;
using Domain.Entities;
using Domain.Identity.Entities;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public RefreshTokenService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<Guid> CreateRefreshToken(AppUser appUser)
        {
            var refreshToken = new RefreshToken()
            {
                
            }


            throw new NotImplementedException();
        }

        public void SetRefreshTokenInCookie(Guid refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
