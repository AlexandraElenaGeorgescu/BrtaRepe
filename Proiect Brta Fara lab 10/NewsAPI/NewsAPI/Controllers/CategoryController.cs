using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        ICategoryCollectionservice categoriesCollectionService;

        public CategoryController(ICategoryCollectionservice categoriesCollectionService)
        {
            this.categoriesCollectionService = categoriesCollectionService ?? throw new ArgumentNullException(nameof(Services.CategoryCollectionService));
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>A list of all categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoriesCollectionService.GetAll();
            return Ok(categories);
        }

        /// <summary>
        /// Gets the category with the matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The category with the specified ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await categoriesCollectionService.Get(id);
            return Ok(category);
        }

        /// <summary>
        /// Creates a category and adds to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>A message mentioning if the category was successfully created</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            category.Id = Guid.NewGuid();
            var result = await categoriesCollectionService.Create(category);
            if (result == false)
            {
                return Ok(null);
            }
            return Ok(category);
        }

        /// <summary>
        /// Updates the category with the matching ID with the new category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns>A message mentioning if the category was successfully updated</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] Category category)
        {
            var result = await categoriesCollectionService.Update(id, category);
            if (result == false)
            {
                return Ok(null);
            }
            return Ok(category);
        }

        /// <summary>
        /// Deletes the category with the matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A message mentioning if the announcement was successfully deleted</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deleted = await categoriesCollectionService.Get(id);
            var result = await categoriesCollectionService.Delete(id);

            if (result == false)
            {
                return Ok(null);
            }
            return Ok(deleted);
        }
    }
}
