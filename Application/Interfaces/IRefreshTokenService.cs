using Domain.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRefreshTokenService
    {
        public void SetRefreshTokenInCookie();
        public Task<Guid> CreateRefreshToken(AppUser appUser);
    }
}
