using ETradeBackend.Application.Exceptions;
using ETradeBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        public LoginUserCommandHandle(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException();
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            if (result.Succeeded)
            {
                //Todo yetkiler
            }
            return new();
        }
    }
}
