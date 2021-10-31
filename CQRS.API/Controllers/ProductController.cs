using AutoMapper;
using CQRS.Application;
using CQRS.Application.Requests.ProductRequests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.ProductRequests.ProductCreateRequest;
using static CQRS.Application.Requests.ProductRequests.ProductUpdateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISystemAppService _systemAppService;
        public ProductController(ISystemAppService systemAppService)
        {
            _systemAppService = systemAppService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            var validator = new ProductCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await _systemAppService.CreateProduct(request);
                return Ok(commandResult.Id);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest request)
        {
            var validator = new ProductUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await _systemAppService.UpdateProduct(request);
                return Ok(commandResult.Id);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(Guid id)
        {
            return Ok(await _systemAppService.GetProductDetail(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _systemAppService.GetProducts());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return Ok(await _systemAppService.DeleteProduct(id));
        }

    }
}
