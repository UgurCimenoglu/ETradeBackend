using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Product.GetAllProductsByQuery
{
    public class GetAllProductByQueryHandler : IRequestHandler<GetAllProductByQueryRequest, GetAllProductByQueryResponse>
    {
        public async Task<GetAllProductByQueryResponse> Handle(GetAllProductByQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
