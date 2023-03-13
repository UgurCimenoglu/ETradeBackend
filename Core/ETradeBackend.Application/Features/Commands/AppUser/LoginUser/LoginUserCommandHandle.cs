using ETradeBackend.Application.Abstracts.Token;
using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;

namespace ETradeBackend.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        public LoginUserCommandHandle(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);
            return new LoginUserSuccesCommandResponse()
            {
                Token = token
            };
        }
    }
}
