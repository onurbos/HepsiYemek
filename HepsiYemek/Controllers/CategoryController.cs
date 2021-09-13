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
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly ICacheExtensions _cacheExtensions;

        public CategoryController(ICategoryRepository categoryRepository, IDistributedCache distributedCache,
            ICacheExtensions cacheExtensions)
        {
            _categoryRepository = categoryRepository;
            _distributedCache = distributedCache;
            _cacheExtensions = cacheExtensions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var cacheResult = await _cacheExtensions.GetRecordAsync(_distributedCache, id);

            if (cacheResult != null) return Ok(cacheResult.ToJson());

            cacheResult = _categoryRepository.GetByIdAsBsonDoc(id);
            if (cacheResult == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

            await _cacheExtensions.SetRecordAsync(_distributedCache, id, cacheResult);

            return Ok(cacheResult.ToJson());
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(category.Id))
            {
                var categoryAsBsonDoc = _categoryRepository.Get(category.Id);
                if (categoryAsBsonDoc != null) return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _categoryRepository.Add(category);
            await _cacheExtensions.SetRecordAsync(_distributedCache, category.Id, category.ToBsonDocument());

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Category category)
        {
            if (!ModelState.IsValid) return new StatusCodeResult(StatusCodes.Status400BadRequest);


            var result = _categoryRepository.Get(category.Id);
            if (result == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

            _categoryRepository.Update(category);
            await _cacheExtensions.SetRecordAsync(_distributedCache, category.Id, category);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var category = _categoryRepository.Get(id);
            if (category == null) return new StatusCodeResult(StatusCodes.Status404NotFound);

            _categoryRepository.Delete(id);
            await _cacheExtensions.Remove(_distributedCache, id);

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}