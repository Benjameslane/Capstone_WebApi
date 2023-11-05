using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        // GET: api/FoodItem/expiring
        [HttpGet("expiring")]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetExpiringFoodItems()
        {
            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var foodItems = await _foodItemService.GetExpiringFoodItemsAsync(userId);

            if (foodItems == null)
            {
                return NotFound();
            }

            return StatusCode(200, foodItems);
        }

        // POST: api/FoodItem
        [HttpPost]
        [Authorize]  // make sure to grab the token from logged in user
        public async Task<ActionResult<FoodItem>> AddFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            
            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var foodItem = await _foodItemService.AddFoodItemAsync(foodItemDto, userId);

            if (foodItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetFoodItem), new { id = foodItem.Id }, foodItem);
        }

        // GET: api/FoodItem/myInventory
        [HttpGet("myInventory")]
        [Authorize]  // make sure to grab the token from logged in user
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetUserInventory()
        {

            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userInventory = await _foodItemService.GetUserInventoryAsync(userId);

            if (userInventory == null)
            {
                return BadRequest();
            }

            return StatusCode(200, userInventory);
        }

        // PUT: api/FoodItem/5
        [HttpPut("{foodId}")]
        public async Task<ActionResult<FoodItem>> UpdateFoodItem(int foodId, [FromBody] FoodItemDto foodItemDto)
        {
            string userId = User.FindFirstValue("id");
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var foodItem = await _foodItemService.UpdateFoodItemByIdAsync(foodId, foodItemDto, userId);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // DELETE: api/FoodItem/5
        [HttpDelete("{foodId}")]
        public async Task<ActionResult<FoodItem>> DeleteFoodItem(int foodId)
        {
            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var foodItem = await _foodItemService.DeleteFoodItemByIdAsync(foodId, userId);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }
    }
}
