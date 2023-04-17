using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Exceptions;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
            {
                throw new PasswordChangeFailedException("Şifre ve şifre tekrarı uyuşmumyor, lütfen kontrol ediniz!");
            }
            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
            return new UpdatePasswordCommandResponse();
        }
    }
}
