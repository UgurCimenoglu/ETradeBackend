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
    public class ProductHubService : IProductHubService
    {
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProcuctAddedMessageAsync(string message)
        {
            _hubContext.Clients.All.SendAsync(RecieveFunctionNames.ProductAddedMessage, message);
        }
    }
}
