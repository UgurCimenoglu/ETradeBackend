using ETradeBackend.Application.Constants;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using ETradeBackend.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ETradeBackend.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.AuthorizationEndpoints, ActionType = ActionType.Reading, Definition = "Get Roles To Endpoint")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.AuthorizationEndpoints, ActionType = ActionType.Writing, Definition = "Assign Role Endpoint")]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest request)
        {
            request.Type = typeof(Program);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
