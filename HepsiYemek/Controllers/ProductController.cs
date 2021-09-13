using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using HepsiYemek.Entities;
using HepsiYemek.Extensions;
using HepsiYemek.Repositories;
using MongoDB.Bson;

namespace HepsiYemek.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        //private readonly IDatabase _dataBase;
        private readonly IDistributedCache _distributedCache;
        private readonly ICacheExtensions _cacheExtensions;

        public ProductController(IProductRepository productRepository, IDistributedCache distributedCache, ICacheExtensions cacheExtensions)
        {
            _productRepository = productRepository;
            //_dataBase = database;
            _distributedCache = distributedCache;
            _cacheExtensions = cacheExtensions;
        }


        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var cacheResult = await _cacheExtensions.GetRecordAsync(_distributedCache, id);

            if (cacheResult == null)
            {
                cacheResult = _productRepository.GetByIdAsBsonDoc(id);

                if (cacheResult == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

                await _cacheExtensions.SetRecordAsync(_distributedCache, id, cacheResult);
            }

            ;
            return Ok(cacheResult.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            if (!ModelState.IsValid) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            if (!string.IsNullOrEmpty(product.Id))
            {
                var productAsBsonDoc = _productRepository.Get(product.Id);
                if (productAsBsonDoc != null) return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _productRepository.Add(product);
            await _cacheExtensions.SetRecordAsync(_distributedCache, product.Id, product);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var product = _productRepository.Get(id);
            if (product == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

            _productRepository.Delete(id);
            //await CacheExtensions.Remove(_distributedCache, id);

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            if (!ModelState.IsValid) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var productAsBsonDoc = _productRepository.Get(product.Id);
            if (productAsBsonDoc == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

            _productRepository.Update(product);
            await _cacheExtensions.SetRecordAsync(_distributedCache, product.Id, product.ToBsonDocument());

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}