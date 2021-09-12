using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using HepsiYemek.Entities;
using HepsiYemek.Extensions;
using HepsiYemek.Repositories;

namespace HepsiYemek.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IDistributedCache _distributedCache;

        public CategoryController(ICategoryRepository categoryRepository, IDistributedCache distributedCache)
        {
            _categoryRepository = categoryRepository;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Please enter a valid id");

            var cacheResult = await CacheExtensions.GetRecordAsync<Category>(_distributedCache, id);

            if (cacheResult == null)
            {
                cacheResult = _categoryRepository.Get(id);
                await CacheExtensions.SetRecordAsync(_distributedCache, id, cacheResult);
            }

            return Ok(cacheResult);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(category.Id))
            {
                var exist = _categoryRepository.Get(category.Id);
                if (exist != null) return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _categoryRepository.Add(category);
            await CacheExtensions.SetRecordAsync(_distributedCache, category.Id, category);

            return Ok(Response.StatusCode);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _categoryRepository.Update(category);
            await CacheExtensions.SetRecordAsync(_distributedCache, category.Id, category);

            return Ok(Response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Please enter a valid id");

            _categoryRepository.Delete(id);
            await CacheExtensions.Remove(_distributedCache, id);

            return NoContent();
        }
    }
}
