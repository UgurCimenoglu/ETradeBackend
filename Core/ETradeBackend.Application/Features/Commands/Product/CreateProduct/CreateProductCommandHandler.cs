using System.Text.Json;
using ETradeBackend.Application.Abstracts.Hubs;
using ETradeBackend.Application.Features.Commands.Product.UpdateProduct;
using ETradeBackend.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETradeBackend.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _logger = logger;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            });
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation(JsonSerializer.Serialize(request), "Ürün Eklendi.");
            await _productHubService.ProcuctAddedMessageAsync($"{request.Name} adlı ürün eklendi.");
            return new();
        }
    }
}
