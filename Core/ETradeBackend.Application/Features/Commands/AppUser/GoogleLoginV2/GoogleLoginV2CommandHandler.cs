using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.GoogleLoginV2
{
    public class GoogleLoginV2CommandHandler : IRequestHandler<GoogleLoginV2CommandRequest, GoogleLoginV2CommandResponse>
    {
        private readonly IAuthService _authService;

        public GoogleLoginV2CommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginV2CommandResponse> Handle(GoogleLoginV2CommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.GoogleLoginV2Async(request.Code);
            return new()
            {
                Token = result
            };
        }
    }
}
