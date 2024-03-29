﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Basket.RemoveItemToBasket
{
    public class RemoveItemToBasketCommandRequest : IRequest<RemoveItemToBasketCommandResponse>
    {
        public string BasketItemId { get; set; }
    }
}
