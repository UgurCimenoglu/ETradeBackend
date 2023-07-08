using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.DTOs.User;
using ETradeBackend.Domain.Entities.Identity;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(AppUser user, string refreshToken, DateTime accessTokenDateTime, int addOnAccessTokenDateTime);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string userName, string code);


    }
}
