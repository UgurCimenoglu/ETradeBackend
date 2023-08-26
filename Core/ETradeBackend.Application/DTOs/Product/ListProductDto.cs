using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Domain.Entities;

namespace ETradeBackend.Application.DTOs.Product
{
    public class ListProductDto
    {
        public ListProductDto()
        {
            Products = new List<ProductListDto>();
        }
        public int TotalCount { get; set; }
        public List<ProductListDto>? Products { get; set; }
    }

    public class ProductListDto
    {
        public ProductListDto()
        {
            ProductImageFiles = new List<ProductImageFile>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }

    }

}
