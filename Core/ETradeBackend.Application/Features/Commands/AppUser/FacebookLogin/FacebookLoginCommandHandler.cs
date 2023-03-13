using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Abstracts.Token;
using ETradeBackend.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Application.Features.Commands.AppUser.FacebookLogin
{
    public class
        FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken);
            return new()
            {
                Token = token
            };

        }
    }
}
