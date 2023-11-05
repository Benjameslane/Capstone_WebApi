using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Services;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.DataTransferObjects;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    { 
        private readonly SpoonacularService _spoonacularService;

        public RecipeController(SpoonacularService spoonacularService)
        {
            _spoonacularService = spoonacularService ?? throw new ArgumentNullException(nameof(spoonacularService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRecipeDto>>> GetRecipeByIngredients([FromQuery] string ingredients)
        {
            try
            {
                var userRecipes = await _spoonacularService.GetRecipesByIngredientsAsync(ingredients);
                return Ok(userRecipes);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
