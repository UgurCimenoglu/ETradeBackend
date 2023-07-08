using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Repositories.ProductRepository;
using ETradeBackend.Domain.Entities;

namespace ETradeBackend.Persistance.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IQRCodeService _qrCodeService;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null) throw new Exception("Product Not Found!");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate
            };

            string plainText = JsonSerializer.Serialize(plainObject);

            return _qrCodeService.GenerateQRCodeAsync(plainText);
        }
    }
}
