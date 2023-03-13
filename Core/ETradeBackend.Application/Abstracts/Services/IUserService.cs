﻿using System;
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
        Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenDateTime, int addOnAccessTokenDateTime);
    }
}
