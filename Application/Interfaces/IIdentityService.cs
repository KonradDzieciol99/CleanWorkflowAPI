using Application.Dtos;
using Domain.Identity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces;

public interface IIdentityService
{
    //Task<string> GetUserNameAsync(string userId);

    //Task<bool> IsInRoleAsync(string userId, string role);

    //Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<AppUser> CreateUserAsync(string email,string password);
    Task<AppUser> SignInAsync(string email,string password);

    //Task<AppUser> DeleteUserAsync(string userId);
}
