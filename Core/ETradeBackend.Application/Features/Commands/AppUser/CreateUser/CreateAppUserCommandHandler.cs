using ETradeBackend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;

namespace ETradeBackend.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateAppUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var createUserResponse = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Fullname = request.Fullname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
            });
            return new()
            {
                Message = createUserResponse.Message,
                Succeeded = createUserResponse.Succeeded
            };
        }
    }
}
