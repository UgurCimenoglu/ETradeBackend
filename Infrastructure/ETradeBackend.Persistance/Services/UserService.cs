using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.DTOs.User;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Application.Features.Commands.AppUser.CreateUser;
using ETradeBackend.Application.Helpers;
using ETradeBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = model.Fullname,
                UserName = model.Username,
                Email = model.Email,
            }, model.Password);
            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla Oluşturuldu.";
            else
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description} \n";
                }
            return response;
        }

        public async Task UpdateRefreshTokenAsync(AppUser user, string refreshToken, DateTime accessTokenDateTime, int refreshTokenLifeTimeMinutes)
        {

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDateTime.AddMinutes(refreshTokenLifeTimeMinutes);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                TwoFactorEnabled = user.TwoFactorEnabled
            }).ToList();

        }

        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userCurrentRoles = await _userManager.GetRolesAsync(user);
                var identityResult = await _userManager.RemoveFromRolesAsync(user, userCurrentRoles);
                if (identityResult.Succeeded)
                {     await _userManager.AddToRolesAsync(user, roles);
                }
                else
                {
                    throw new Exception(identityResult.Errors.ToString());
                }
            }
            else
            {
                throw new NotFoundUserException();
            }

        }

        public async Task<List<string>> GetRolesToUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }

            throw new Exception("");
        }
    }
}

