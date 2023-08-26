using ETradeBackend.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IProductService
    {
        ListProductDto GetProductListAsync(int page, int size);
        Task<byte[]> QRCodeToProductAsync(string productId);
    }
}
