using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.ViewModels.Basket;
using ETradeBackend.Domain.Entities;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VM_Create_Basketitem basketItem);
        public Task UpdateQuantityAsync(VM_Update_Basketitem basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}
