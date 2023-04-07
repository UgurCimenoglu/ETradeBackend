using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Hubs;
using ETradeBackend.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETradeBackend.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message) =>
            await _hubContext.Clients.All.SendAsync(RecieveFunctionNames.OrderAddedMessage, message);
    }
}
