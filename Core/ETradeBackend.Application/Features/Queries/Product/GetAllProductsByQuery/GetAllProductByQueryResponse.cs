using ETradeBackend.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProductsByQuery
{
    public class GetAllProductByQueryResponse
    {
        public int TotalCount { get; set; }
        public List<ProductListDto>? Products { get; set; }
        public string? Q { get; set; }
    }
}
