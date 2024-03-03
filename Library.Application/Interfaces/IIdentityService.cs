using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string cuurentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> RoleExistsAsync(string role);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> VerifyResetPasswordTokenAsync(ApplicationUser user, string token);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<bool> UserIsInRoleAsync(ApplicationUser user, string role);
    }
}
