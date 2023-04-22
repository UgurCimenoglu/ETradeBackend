using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.DTOs.Order;
using ETradeBackend.Application.Repositories.CompletedOrder;
using ETradeBackend.Application.Repositories.OrderRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Infrastructure.Operations;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        private readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Description = createOrder.Description,
                Id = Guid.Parse(createOrder.BasketId),
                //OrderCode = (new Random().NextDouble() * 100000000).ToString()
                OrderCode = RandomAlphaNumeric.GetRandomAlphaNumeric()
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var totalCount = _orderReadRepository.Table.Count();
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.AppUser)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

            var data = query
                .Skip(page * size)
                .Take(size);

            var result = (from order in data
                          join completedOrder in _completedOrderReadRepository.Table
                              on order.Id equals completedOrder.OrderId into co
                          from _co in co.DefaultIfEmpty()
                          select new OrderList()
                          {
                              Id = order.Id.ToString(),
                              UserName = order.Basket.AppUser.FullName,
                              OrderCode = order.OrderCode,
                              CreatedDate = order.CreatedDate,
                              TotalPrice = order.Basket.BasketItems.Sum(bi => bi.Product.Price),
                              Completed = _co != null,
                          }).ToListAsync();
            return new()
            {
                Orders = await result,
                TotalCount = totalCount,
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity,
                }).ToList(),
                Address = data.Address,
                OrderCode = data.OrderCode,
                CreatedDate = data.CreatedDate,
                Description = data.Description
            };
        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order is not null)
            {
                await _completedOrderWriteRepository.AddAsync(new CompletedOrder() { OrderId = Guid.Parse(id) });
                await _completedOrderWriteRepository.SaveAsync();
            }
        }
    }
}
