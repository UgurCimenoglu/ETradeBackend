using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Features.Commands.AppUser.CreateUser;
using ETradeBackend.Application.Features.Commands.AppUser.FacebookLogin;
using ETradeBackend.Application.Features.Commands.AppUser.GoogleLogin;
using ETradeBackend.Application.Features.Commands.AppUser.LoginUser;
using ETradeBackend.Application.Features.Commands.AppUser.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAppUserCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SendExampleMail()
        {
            await _mailService.SendMailAsync("ugurcimenogluu@gmail.com", "Test Mail",
                "<strong>Bu bir test mailidir</strong>", true);
            return Ok();
        }
        
    }
}
