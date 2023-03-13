using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.DTOs.User;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Application.Features.Commands.AppUser.CreateUser;
using ETradeBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

        public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenDateTime, int refreshTokenLifeTimeMinutes)
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
    }
}

