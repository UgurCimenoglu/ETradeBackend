﻿using ETradeBackend.Application.Features.Commands.AppUser.FacebookLogin;
using ETradeBackend.Application.Features.Commands.AppUser.GoogleLogin;
using ETradeBackend.Application.Features.Commands.AppUser.GoogleLoginV2;
using ETradeBackend.Application.Features.Commands.AppUser.LoginUser;
using ETradeBackend.Application.Features.Commands.AppUser.PasswordReset;
using ETradeBackend.Application.Features.Commands.AppUser.RefreshTokenLogin;
using ETradeBackend.Application.Features.Commands.AppUser.UpdatePassword;
using ETradeBackend.Application.Features.Commands.AppUser.VerifyPasswordResetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("google-login-v2")]
        public async Task<IActionResult> GoogleLoginV2(GoogleLoginV2CommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> GoogleLogin(FacebookLoginCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenLoginCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyPasswordResetTokenCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
