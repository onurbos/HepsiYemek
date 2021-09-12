using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using HepsiYemek.Entities;
using HepsiYemek.Extensions;
using HepsiYemek.Repositories;

namespace HepsiYemek.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        //private readonly ICategoryRepository _categoryRepository;
        //private readonly IDatabase _dataBase;
        private readonly IDistributedCache _distributedCache;

        public ProductController(IProductRepository productRepository, IDistributedCache distributedCache )
        {
            _productRepository = productRepository;
            //_dataBase = database;
            _distributedCache = distributedCache;
            // _categoryRepository = categoryRepository;
        }


        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Please enter a valid id");

            var cacheResult = await CacheExtensions.GetRecordAsync<Product>(_distributedCache, id);

            if (cacheResult == null)
            {
                cacheResult = _productRepository.Get(id);
                await CacheExtensions.SetRecordAsync(_distributedCache, id, cacheResult);
            }

            return Ok(cacheResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(product.Id))
            {
                var exist = _productRepository.Get(product.Id);
                if (exist != null) return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _productRepository.Add(product);
            await CacheExtensions.SetRecordAsync(_distributedCache, product.Id, product);

            return Ok(Response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Please enter a valid id");

            _productRepository.Delete(id);
            await CacheExtensions.Remove(_distributedCache, id);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _productRepository.Update(product);
            await CacheExtensions.SetRecordAsync(_distributedCache, product.Id, product);

            return Ok(Response.StatusCode);
        }

    }
}
