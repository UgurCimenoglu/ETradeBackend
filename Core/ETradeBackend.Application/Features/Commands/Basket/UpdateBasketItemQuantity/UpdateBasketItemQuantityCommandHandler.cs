using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;
using MediatR.Pipeline;

namespace ETradeBackend.Application.Features.Commands.Basket.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandHandler : IRequestHandler<UpdateBasketItemQuantityCommandRequest, UpdateBasketItemQuantityCommandResponse>
    {
        private readonly IBasketService _basketService;

        public UpdateBasketItemQuantityCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }


        public async Task<UpdateBasketItemQuantityCommandResponse> Handle(UpdateBasketItemQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantityAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity
            });
            return new();
        }
    }
}
