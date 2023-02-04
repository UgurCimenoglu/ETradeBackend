using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Features.Queries.ProductImaageFile.GetProductImages
{
    public class GetProductImageQueryResponse
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
