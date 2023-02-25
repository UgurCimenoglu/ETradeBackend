using ETradeBackend.Application.Abstracts.Storage;
using ETradeBackend.Application.Features.Commands.Product.CreateProduct;
using ETradeBackend.Application.Features.Commands.Product.RemoveProduct;
using ETradeBackend.Application.Features.Commands.Product.UpdateProduct;
using ETradeBackend.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETradeBackend.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETradeBackend.Application.Features.Queries.Product.GetAllProducts;
using ETradeBackend.Application.Features.Queries.Product.GetByIdProduct;
using ETradeBackend.Application.Features.Queries.ProductImaageFile.GetProductImages;
using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Application.Repositories.InvoiceFileRepository;
using ETradeBackend.Application.Repositories.ProductImageFileRepository;
using ETradeBackend.Application.Repositories.ProductRepository;
using ETradeBackend.Application.ViewModels.Products;
using ETradeBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
        {
            request.FormFiles = Request.Form.Files;
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest request, [FromQuery] string imageId)
        {
            request.ImageId = imageId;
            await _mediator.Send(request);
            return Ok();
        }
    }

}
