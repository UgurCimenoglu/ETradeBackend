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
        ListProductDto GetProductList(int page, int size);

        ListProductByQueryDto GetProductListByQuery(int page, int size, string? query);

        Task<byte[]> QRCodeToProductAsync(string productId);
    }
}
