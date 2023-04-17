using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.VerifyPasswordResetToken
{
    public class VerifyPasswordResetTokenCommandHandler : IRequestHandler<VerifyPasswordResetTokenCommandRequest, VerifyPasswordResetTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public VerifyPasswordResetTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyPasswordResetTokenCommandResponse> Handle(VerifyPasswordResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
            return new()
            {
                State = state
            };
        }
    }
}
