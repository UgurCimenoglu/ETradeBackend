using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Hubs;
using ETradeBackend.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHubService, ProductHubService>();
            services.AddScoped<IOrderHubService, OrderHubService>();
            services.AddSignalR();
        }
    }
}
