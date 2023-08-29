using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProductsByQuery
{
    public class GetAllProductByQueryHandler : IRequestHandler<GetAllProductByQueryRequest, GetAllProductByQueryResponse>
    {
        private readonly IProductService _productService;

        public GetAllProductByQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetAllProductByQueryResponse> Handle(GetAllProductByQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = _productService.GetProductListByQuery(request.Page, request.Size, request.Q);
            return new()
            {
                Products = datas.Products,
                Q = datas.Q,
                TotalCount = datas.TotalCount,
            };
        }
    }
}
