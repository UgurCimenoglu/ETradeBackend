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

namespace ETradeBackend.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandle(UserManager<Domain.Entities.Identity.AppUser> userManager,
            SignInManager<Domain.Entities.Identity.AppUser> signInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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
                Token token = _tokenHandler.CreateAccessToken(tokenExpirationMinute: 30);
                return new LoginUserSuccesCommandResponse()
                {
                    Token = token
                };
                //Todo yetkiler
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanici adi veya sifre hatali!"
            //};
            throw new AuthenticationErrorException();
        }
    }
}
