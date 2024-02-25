using Library.Application.Dtos.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAysnc(RegisterModel model);
        Task<AuthModel> LoginAsync(TokenrRequestModel model);
        Task<AuthModel> VerifyAccountAsync(VerifyAccountModel model);
        Task<ResetTokenModel> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<AuthModel> ResetPasswordAsync(ResetPasswordModel model);
        Task<AuthModel> ChangePasswordAsync(string userId, ChangePasswordModel passwordDto);
        Task<string> AddRoleAysnc(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
