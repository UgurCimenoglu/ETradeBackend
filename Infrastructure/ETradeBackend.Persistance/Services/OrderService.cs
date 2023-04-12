using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.DTOs.Order;
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

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
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
            var pagingOrders = await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.AppUser)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .Select(o => new OrderList
                {
                    Id = o.Id.ToString(),
                    UserName = o.Basket.AppUser.FullName,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price),
                    CreatedDate = o.CreatedDate

                })
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
            return new()
            {
                Orders = pagingOrders,
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
    }
}
