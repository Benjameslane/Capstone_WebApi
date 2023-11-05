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
    public class StoreFoodItemController : ControllerBase
    {
        private readonly StoreFoodItemService _storeFoodItemService;

        public StoreFoodItemController(StoreFoodItemService storeFoodItemService)
        {
            _storeFoodItemService = storeFoodItemService;
        }

        // GET: api/StoreFoodItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreFoodItem>> GetStoreFoodItem(int id)
        {
            var storeFoodItem = await _storeFoodItemService.GetStoreFoodItemByIdAsync(id);

            if (storeFoodItem == null)
            {
                return NotFound();
            }
            return storeFoodItem;
        }

        // POST: api/StoreFoodItem
        [HttpPost]
        [Authorize]  // make sure to grab the token from logged in user
        public async Task<ActionResult<StoreFoodItem>> AddStoreFoodItem([FromBody] StoreFoodItemDto storeFoodItemDto)
        {

            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var storeFoodItem = await _storeFoodItemService.AddStoreFoodItemAsync(storeFoodItemDto, userId);

            if (storeFoodItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetStoreFoodItem), new { id = storeFoodItem.Id }, storeFoodItem);
        }

        // GET: api/StoreFoodItem/storeInventory
        [HttpGet("storeInventory")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StoreFoodItem>>> GetStoreInventory()
        {

            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userInventory = await _storeFoodItemService.GetStoreInventoryAsync(userId);

            if (userInventory == null)
            {
                return BadRequest();
            }

            return StatusCode(200, userInventory);
        }

        // GET: api/StoreFoodItem/storeInventory/discounted/byStoreOwner?id={id}
        [HttpGet("storeInventory/discounted/byStoreOwner")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StoreFoodItem>>> GetDiscountedStoreInventoryByStoreOwner([FromQuery] string id)
        {
            var userInventory = await _storeFoodItemService.GetDiscountedStoreInventoryAsync(id);

            if (userInventory == null)
            {
                return BadRequest();
            }

            return StatusCode(200, userInventory);
        }

        // GET: api/StoreFoodItem/listed
        [HttpGet("listed")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StoreFoodItem>>> GetListedStoreFoodItems()
        {

            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var listedStoreFoodItems = await _storeFoodItemService.GetListedStoreFoodItemsAsync(userId);

            if (listedStoreFoodItems == null)
            {
                return BadRequest();
            }

            return StatusCode(200, listedStoreFoodItems);
        }

        // GET: api/StoreFoodItem/discounted
        [HttpGet("discounted")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StoreFoodItem>>> GetDiscountedStoreFoodItems()
        {

            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userInventory = await _storeFoodItemService.GetDiscountedStoreFoodItemsAsync(userId);

            if (userInventory == null)
            {
                return BadRequest();
            }

            return StatusCode(200, userInventory);
        }

        // PUT: api/StoreFoodItem/5
        [HttpPut("{foodId}")]
        public async Task<ActionResult<StoreFoodItem>> UpdateStoreFoodItem(int foodId, [FromBody] StoreFoodItemDto storeFoodItemDto)
        {
            string userId = User.FindFirstValue("id");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var storeFoodItem = await _storeFoodItemService.UpdateStoreFoodItemByIdAsync(foodId, storeFoodItemDto, userId);

            if (storeFoodItem == null)
            {
                return NotFound();
            }

            return storeFoodItem;
        }

        // DELETE: api/StoreFoodItem/5
        [HttpDelete("{foodId}")]
        public async Task<ActionResult<StoreFoodItem>> DeleteStoreFoodItem(int foodId)
        {
            string userId = User.FindFirstValue("id");
            var storeFoodItem = await _storeFoodItemService.DeleteStoreFoodItemByIdAsync(foodId, userId);

            if (storeFoodItem == null)
            {
                return NotFound();
            }

            return storeFoodItem;
        }
    }
}
