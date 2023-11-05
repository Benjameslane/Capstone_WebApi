/*using FullStackAuth_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Models;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly SpoonacularService _spoonacularService;
        private readonly FoodItemService _foodItemService;

        public RecipesController(SpoonacularService spoonacularService, FoodItemService foodItemService)
        {
            _spoonacularService = spoonacularService;
            _foodItemService = foodItemService;
        }

        [HttpGet("by-expiring-items")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByExpiringItems([FromQuery] int days = 7)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // or whatever claim type you are using for userId
            if (userId == null)
            {
                return BadRequest("UserID claim is missing");
            }

            var expiringItems = await _foodItemService.GetExpiringFoodItemsAsync(userId, days);
            var ingredientNames = expiringItems.Select(item => item.ItemName);
            var recipes = await _spoonacularService.GetRecipesByIngredientsAsync(ingredientNames);
            return Ok(recipes);
        }
    }
}*/