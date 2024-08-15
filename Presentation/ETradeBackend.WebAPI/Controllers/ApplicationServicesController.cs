using ETradeBackend.Application.Abstracts.Services.Configurations;
using ETradeBackend.Application.Constants;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ApplicationServices, ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var result = _applicationService.GetAuthorizationDefinitionEndpoints(typeof(Program));
            return Ok(result);
        }
    }
}
