using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Basket.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandRequest : IRequest<UpdateBasketItemQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
