using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = refreshToken
            };
        }
    }
}
