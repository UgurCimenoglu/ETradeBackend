using ETradeBackend.Application.Repositories.ProductRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductService _productService;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository, IProductService productService)
        {
            _productReadRepository = productReadRepository;
            _productService = productService;
        }

        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = _productService.GetProductListAsync(request.Page, request.Size);
            return new() { Products = response.Products, TotalCount = response.TotalCount };
            
        }
    }
}
