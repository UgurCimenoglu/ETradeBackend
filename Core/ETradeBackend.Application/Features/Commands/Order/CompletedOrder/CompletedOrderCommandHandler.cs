using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Order.CompletedOrder
{
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompletedOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.Id);
            return new();
        }
    }
}
