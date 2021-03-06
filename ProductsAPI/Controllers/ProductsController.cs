using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProductsAPI.BusinessServices.Interfaces;
using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private readonly IMemoryCache _cache;
        private const string allProductsCacheKey = "allProducts";

        public ProductsController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _cache = memoryCache;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            //var products = _productService.GetProducts();
            if (!_cache.TryGetValue(allProductsCacheKey, out List<ProductModel> products))
            {
                products = _productService.GetProducts().ToList();
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(allProductsCacheKey, products, cacheExpiryOptions);
            }

            return products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ProductModel Get(long id)
        {
            ProductModel product = null;
            if (_cache.TryGetValue(allProductsCacheKey, out List<ProductModel> products))
            {
                product = products.FirstOrDefault(a => a.Id == id);
            }
            else
            {
                product = _productService.GetProductById(id);
            }

            return product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ProductModel product)
        {
            _cache.Remove(allProductsCacheKey);

            try
            {
                _productService.AddProduct(product);

            }
            catch (Exception ex)
            {
                //todo handle teh exceptions
                //logging
                throw;
            }

            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(long id, [FromBody] ProductModel product)
        {
            _cache.Remove(allProductsCacheKey);
            try
            {
                product.Id = id;
                _productService.UpdateProduct(product);

            }
            catch (Exception ex)
            {
                //todo handle the exceptions
                //logging
                throw;
            }

            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            _cache.Remove(allProductsCacheKey);
         
            try
            {
                _productService.DeleteProduct(id);

            }
            catch (Exception ex)
            {
                //todo handle the exceptions
                //logging
                throw;
            }

            return Ok();

        }
    }
}
