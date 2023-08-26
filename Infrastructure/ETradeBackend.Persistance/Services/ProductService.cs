using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.DTOs.Product;
using ETradeBackend.Application.Repositories.ProductRepository;
using ETradeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public ListProductDto GetProductListAsync(int page, int size)
        {
            var totalProductCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(page * size).Take(size)
                .Include(p => p.ProductImageFiles)
                .Select(p => new ProductListDto()
                {

                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    ProductImageFiles = p.ProductImageFiles
                }).ToList();

            return new() { Products = products, TotalCount = totalProductCount };
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
