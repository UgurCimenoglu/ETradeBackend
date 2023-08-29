using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.DTOs.Product
{
    public class ListProductByQueryDto : ListProductDto
    {
        public string? Q { get; set; }
    }
}
