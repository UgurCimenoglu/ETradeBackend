using ETradeBackend.Application.Abstracts;
using ETradeBackend.Application.Repositories.CustomerRepository;
using ETradeBackend.Application.Repositories.OrderRepository;
using ETradeBackend.Application.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            // await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){Id=Guid.NewGuid(),Name="Laptop",CreatedDate=DateTime.UtcNow,Price=10000,Stock=39},
            //    new(){Id=Guid.NewGuid(),Name="Mouse",CreatedDate=DateTime.UtcNow,Price=500,Stock=19},
            //    new(){Id=Guid.NewGuid(),Name="Keyboard",CreatedDate=DateTime.UtcNow,Price=1000,Stock=29}
            //});
            // await _productWriteRepository.SaveAsync();
            // return Ok("Ekleme Başarılı");
            var customerId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Ugur Cimen" });
            await _orderWriteRepository.AddAsync(new() { Description = "Sipariş Alındı!", Address = "İstanbul", CustomerId = customerId });
            await _orderWriteRepository.AddAsync(new() { Description = "Sipariş Alındı!", Address = "Ankara", CustomerId = customerId });
            await _orderWriteRepository.SaveAsync();
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
