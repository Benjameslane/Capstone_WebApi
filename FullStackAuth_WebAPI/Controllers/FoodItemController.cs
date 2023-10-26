using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController : ControllerBase
    {
        private readonly FoodItemService _foodItemService;

        public FoodItemController(FoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        // GET: api/FoodItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItem>> GetFoodItem(int id)
        {
            var foodItem = await _foodItemService.GetFoodItemByIdAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // POST: api/FoodItem
        [HttpPost]
        [Authorize]  // make sure to grab the token from logged in user
        public async Task<ActionResult<FoodItem>> AddFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            
            int userId = 1;

            var foodItem = await _foodItemService.AddFoodItemAsync(foodItemDto, userId);

            if (foodItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetFoodItem), new { id = foodItem.Id }, foodItem);
        }

       
    }
}
