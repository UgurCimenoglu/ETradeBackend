using ETradeBackend.Application.Constants;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using ETradeBackend.Application.Features.Commands.Order.CompletedOrder;
using ETradeBackend.Application.Features.Commands.Order.CreateOrder;
using ETradeBackend.Application.Features.Queries.Order.GetAllOrders;
using ETradeBackend.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("complete-order/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete an Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute] CompletedOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
