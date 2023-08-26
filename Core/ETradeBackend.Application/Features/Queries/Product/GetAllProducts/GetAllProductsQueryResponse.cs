using ETradeBackend.Application.DTOs.Product;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryResponse
    {
        public int TotalCount { get; set; }
        public List<ProductListDto>? Products { get; set; }
    }
}
