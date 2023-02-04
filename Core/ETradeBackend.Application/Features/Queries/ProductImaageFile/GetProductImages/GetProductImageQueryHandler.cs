using ETradeBackend.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Features.Queries.ProductImaageFile.GetProductImages
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private IConfiguration _configuration;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration = null)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImageQueryResponse>?> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return product?.ProductImageFiles.Select(p => new GetProductImageQueryResponse()
            {
                Id = p.Id,
                FileName = p.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}{p.Path}",
            }).ToList();
        }
    }
}
