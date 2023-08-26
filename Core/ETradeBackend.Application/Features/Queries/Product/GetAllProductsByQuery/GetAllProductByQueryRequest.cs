using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProductsByQuery
{
    public class GetAllProductByQueryRequest : IRequest<GetAllProductByQueryResponse>
    {
        public string q { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
